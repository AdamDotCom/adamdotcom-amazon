using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using AdamDotCom.Amazon.Domain;
using Microsoft.ServiceModel.Web;

namespace AdamDotCom.Amazon.Service
{
    public class AmazonService : IAmazon
    {
        public AmazonResponse Reviews(string customerId)
        {
            throw new RestException(HttpStatusCode.Unauthorized, new List<string> {"wrong name"});

            OutgoingWebResponseContext outResponse = WebOperationContext.Current.OutgoingResponse;
            outResponse.StatusCode = HttpStatusCode.BadRequest;
            outResponse.StatusDescription = "hey";
            return null;
            var amazonResponse = new AmazonFactory(BuildRequest(customerId, null)).GetResponse();

            if (amazonResponse.Errors != null && amazonResponse.Errors.Count!=0)
            {
                throw new WebProtocolException(HttpStatusCode.BadRequest, "hey", null);
            }

            return amazonResponse;
        }

        public AmazonResponse Wishlist(string listId)
        {
            return new AmazonFactory(BuildRequest(null, listId)).GetResponse();
        }

        private static AmazonRequest BuildRequest(string customerId, string listId)
        {
            return new AmazonRequest
                       {
                           AssociateTag = "adamkahtavaap-20",
                           AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2",
                           CustomerId = customerId,
                           ListId = listId
                       };
        }
    }

    internal class RestException : Exception
    {
        public RestException(HttpStatusCode httpStatusCode, List<string> errorList)
        {
            var exception = new HttpException((int) httpStatusCode, "Error", 10);
            
            foreach (var item in errorList)
            {
                exception.Data.Add("key",item);
            }

            throw exception;
        }
    }
}