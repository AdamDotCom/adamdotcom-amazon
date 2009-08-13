using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Amazon.Service.Utilities
{
    [DataContract]
    public class RestError
    {
        public RestError()
        {
        }

        public RestError(IDictionary dictionary, int httpStatusCode, int errorCode)
        {
            Errors = new Dictionary<string, string>();
            foreach (DictionaryEntry item in dictionary)
            {
                Errors.Add((string) item.Key, (string) item.Value);
            }

            Description = string.Format("An error {0} occured with a HttpStatusCode {1}.", httpStatusCode, errorCode);
        }

        [DataMember] public IDictionary<string, string> Errors;

        [DataMember] public string Description;
    }
}