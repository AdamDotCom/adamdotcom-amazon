using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Web;

namespace AdamDotCom.Amazon.Service.Utilities
{
    public class WebHttpWithExceptions : WebHttpBehavior
    {
        private static List<WebGetAttribute> webGetAttributes;

        static WebHttpWithExceptions()
        {
            webGetAttributes = new List<WebGetAttribute>();
        }

        protected override IDispatchMessageFormatter GetReplyDispatchFormatter(OperationDescription operationDescription, ServiceEndpoint endpoint)
        {
            var webGetAttribute = operationDescription.Behaviors.Find<WebGetAttribute>();
            webGetAttributes.Add(webGetAttribute);

            return base.GetReplyDispatchFormatter(operationDescription, endpoint);
        }

        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();

            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new ErrorHandler(webGetAttributes));
        }

        public class ErrorHandler : IErrorHandler
        {
            private readonly List<WebGetAttribute> webGetAttributes;

            public ErrorHandler()
            {
            }

            public ErrorHandler(List<WebGetAttribute> webGetAttributes)
            {
                this.webGetAttributes = webGetAttributes;
            }

            public bool HandleError(Exception error)
            {
                return false;
            }

            public void ProvideFault(Exception exception, MessageVersion version, ref Message fault)
            {
                if (exception.GetType() == typeof(HttpException))
                {
                    var httpException = (HttpException)exception;

                    var inResponse = WebOperationContext.Current.IncomingRequest;

                    var webGetAttribute = GetCurrentAttribute(inResponse);
                    
                    if(webGetAttribute == null)
                    {
                        throw new RestException();    
                    }

                    var currentResponseFormat = webGetAttribute.ResponseFormat;

                    var restErrorMessage = new RestErrorMessage(httpException.Data, httpException.GetHttpCode(), httpException.ErrorCode);
                    fault = CreateMessage(restErrorMessage, version, currentResponseFormat);
                    fault.Properties.Add(WebBodyFormatMessageProperty.Name, GetBodyFormat(currentResponseFormat));

                    var outResponse = WebOperationContext.Current.OutgoingResponse;
                    outResponse.StatusCode = (HttpStatusCode)httpException.GetHttpCode();
                    outResponse.ContentType = string.Format("application/{0}", currentResponseFormat);
                }
                else
                {
                    throw new RestException();
                }
            }

            private static WebBodyFormatMessageProperty GetBodyFormat(WebMessageFormat webMessageFormat)
            {
                return new WebBodyFormatMessageProperty((WebContentFormat) Enum.Parse(typeof(WebContentFormat), webMessageFormat.ToString()));
            }

            private static Message CreateMessage(RestErrorMessage restErrorMessage, MessageVersion version, WebMessageFormat webMessageFormat)
            {
                if (webMessageFormat == WebMessageFormat.Json)
                {
                    return Message.CreateMessage(version, null, restErrorMessage, new DataContractJsonSerializer(restErrorMessage.GetType()));
                }
                if (webMessageFormat == WebMessageFormat.Xml)
                {
                    return Message.CreateMessage(version, null, restErrorMessage);
                }                
                return null;
            }

            private WebGetAttribute GetCurrentAttribute(IncomingWebRequestContext inResponse)
            {
                return webGetAttributes.Find(a => a.UriTemplate == inResponse.UriTemplateMatch.Template.ToString());
            }
        }
    }
}