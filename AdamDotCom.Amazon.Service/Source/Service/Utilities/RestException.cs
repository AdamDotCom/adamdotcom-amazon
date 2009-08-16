using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace AdamDotCom.Amazon.Service.Utilities
{
    internal class RestException : Exception
    {
        public RestException()
        {
            throw new HttpException((int)HttpStatusCode.InternalServerError, "Error");
        }

        public RestException(HttpStatusCode httpStatusCode, IEnumerable<KeyValuePair<string, string>> errorList, int errorCode)
        {
            var exception = new HttpException((int) httpStatusCode, "RestException", errorCode);

            foreach (var item in errorList)
            {
                exception.Data.Add(item.Key, item.Value);
            }

            throw exception;
        }
    }
}