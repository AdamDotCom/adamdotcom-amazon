using System.Configuration;
using AdamDotCom.Amazon.Domain;

namespace AdamDotCom.Amazon.UnitTests
{
    public static class TestHelper
    {
        public static string AssociateTag = ConfigurationManager.AppSettings["AssociateTag"];
        public static string AwsAccessKey = ConfigurationManager.AppSettings["AwsAccessKey"];
        public static string SecretAccessKey = ConfigurationManager.AppSettings["SecretAccessKey"];
        public static string CustomerId = "A2JM0EQJELFL69";
        public static string ListId = "3JU6ASKNUS7B8";

        public static AmazonRequest ValidAmazonRequest = new AmazonRequest
                                                             {
                                                                 AssociateTag = AssociateTag,
                                                                 AccessKeyId = AwsAccessKey,
                                                                 SecretAccessKey = SecretAccessKey,
                                                                 CustomerId = CustomerId,
                                                                 ListId = ListId
                                                             };
    }
}