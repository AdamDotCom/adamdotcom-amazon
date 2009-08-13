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

        public RestException(HttpStatusCode httpStatusCode, List<string> errorList)
        {
            var exception = new HttpException((int) httpStatusCode, "Error", 10);

            foreach (string item in errorList)
            {
                exception.Data.Add("key", item);
            }

            throw exception;
        }
    }
}