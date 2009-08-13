using System;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Web;

namespace AdamDotCom.Amazon.Service.Utilities
{
    public class WebHttpWithExceptions : WebHttpBehavior
    {
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();

            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new ErrorHandler());
        }

        public class ErrorHandler : IErrorHandler
        {
            public bool HandleError(Exception error)
            {
                return true;
            }

            public void ProvideFault(Exception exception, MessageVersion version, ref Message fault)
            {
                if (exception.GetType() == typeof(HttpException))
                {
                    var httpException = (HttpException)exception;

                    fault = Message.CreateMessage(version, null, new RestError(httpException.Data, httpException.GetHttpCode(), 10));

                    OutgoingWebResponseContext outResponse = WebOperationContext.Current.OutgoingResponse;
                    outResponse.StatusCode = (HttpStatusCode)httpException.GetHttpCode();
                }
                else
                {
                    throw new RestException();
                }
            }
        }
    }
}