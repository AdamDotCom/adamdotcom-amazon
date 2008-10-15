using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AdamDotCom.Amazon.Application.Interfaces;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;

namespace AdamDotCom.Amazon.Application
{
    public class AmazonApplication : IAmazonApplication
    {
        private readonly IAmazonFactory amazonFactory;
        private readonly IFileParameters fileParameters;

        public AmazonApplication(IAmazonRequest amazonRequest, IFileParameters fileParameters)
        {
            amazonFactory = new AmazonFactory(amazonRequest);
            this.fileParameters = fileParameters;
        }

        public bool Save()
        {
            IAmazonResponse amazonResponse = amazonFactory.GetResponse();

            XmlSerializer serializer;
            TextWriter writer;

            try
            {
                if (amazonResponse.Errors.Count != 0)
                {
                    serializer = new XmlSerializer(typeof (List<string>));
                    writer = new StreamWriter(fileParameters.ErrorFileNameAndPath);
                    serializer.Serialize(writer, amazonResponse.Errors);
                    writer.Close();
                }

                serializer = new XmlSerializer(typeof (List<Review>));
                writer = new StreamWriter(fileParameters.ReviewFileNameAndPath);
                serializer.Serialize(writer, amazonResponse.Reviews);
                writer.Close();

                serializer = new XmlSerializer(typeof (List<Product>));
                writer = new StreamWriter(fileParameters.ProductFileNameAndPath);
                serializer.Serialize(writer, amazonResponse.Products);
                writer.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}