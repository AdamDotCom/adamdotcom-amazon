using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Amazon.Service
{
    [DataContract]
    public class RestErrorMessage
    {
        public RestErrorMessage()
        {
        }

        public RestErrorMessage(IDictionary dictionary, int httpStatusCode, int errorCode)
        {
            Errors = new Errors();
            foreach (DictionaryEntry item in dictionary)
            {
                Errors.Add((string) item.Key, (string) item.Value);
            }

            Description = string.Format("An error {0} occured with a HttpStatusCode of {1}.", errorCode, httpStatusCode);
        }

        [DataMember] public Errors Errors;

        [DataMember] public string Description;
    }

    [CollectionDataContract(Name = "Errors", ItemName = "Error", KeyName="Key", ValueName="Value")]
    public class Errors: Dictionary<string, string>
    {
        public Errors()
        {
        }

        public Errors(IDictionary<string, string> errors) : base(errors)
        {
        }
    }
}