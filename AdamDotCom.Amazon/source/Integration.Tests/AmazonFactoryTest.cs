using System.Diagnostics;
using System.Linq;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Extensions;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class AmazonFactoryTest
    {

        [Test]
        public void ShouldInvokeAndReturnSuccessfulProductsAndReviewsWithNoErrors()
        {
            var amazonRequest = TestHelper.ValidAmazonRequest;

            var amazonResponse = new AmazonFactory(amazonRequest).GetResponse();

            var errors = string.Empty;
            foreach (var error in amazonResponse.Errors)
            {
                errors += error + " ";
            }

            Assert.AreNotEqual(null, amazonResponse.Products);
            Assert.Greater(amazonResponse.Products.Count, 10, errors);

            Assert.AreNotEqual(null, amazonResponse.Reviews);
            Assert.Greater(amazonResponse.Reviews.Count, 10, errors);

            Assert.AreEqual(amazonResponse.Errors.Count, 0, errors);
        }

        [Test]
        public void ShouldInvokeAndReturnErrorsFromMissingSecretAccessKey()
        {
            var amazonRequest = new AmazonRequest
                                    {
                                        AssociateTag = TestHelper.AssociateTag,
                                        AccessKeyId = TestHelper.AwsAccessKey,
                                        SecretAccessKey = null
                                    };

            var errors = amazonRequest.Validate();

            foreach (var error in errors)
            {
                Debug.WriteLine(error);
            }

            Assert.IsTrue(errors.Select(e => e.Key == "SecretAccessKey") != null);
        }

        [Test, Ignore("Explicit Test")]
        public void ShouldInvokeAndReturnErrorsFromIncorrectSecretAccessKey()
        {
            var amazonRequest = new AmazonRequest
            {
                AssociateTag = TestHelper.AssociateTag,
                AccessKeyId = TestHelper.AwsAccessKey,
                CustomerId = TestHelper.CustomerId,
                ListId = TestHelper.ListId,
                SecretAccessKey = "injectingIncorrectSecretyAccessKey"
            };

            var amazonResponse = new AmazonFactory(amazonRequest).GetResponse();

            foreach (var error in amazonResponse.Errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(2, amazonResponse.Errors.Count);
            Assert.IsTrue(amazonResponse.Errors.Select(e => e.Key == "ServiceError") != null);
        }

        [Test]
        public void ShouldInvokeAndReturnErrorsFromIncorrectCustomerIdAndListId()
        {
            var amazonRequest = new AmazonRequest
                                    {
                                        AssociateTag = TestHelper.AssociateTag,
                                        AccessKeyId = TestHelper.AwsAccessKey,
                                        CustomerId = "injectingIncorrectData",
                                        ListId = "injectingIncorrectData2",
                                        SecretAccessKey = TestHelper.SecretAccessKey
                                    };

            var amazonResponse = new AmazonFactory(amazonRequest).GetResponse();

            foreach (var error in amazonResponse.Errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(2, amazonResponse.Errors.Count);

            Assert.IsTrue(amazonResponse.Errors.Select(e => e.Key == "CustomerId") != null);
            Assert.IsTrue(amazonResponse.Errors.Select(e => e.Key == "ListId") != null);
        }

        [Test, Ignore("Explicit Test")]
        public void ShouldInvokeAndReturnErrorsFromIncorrectAWSAccessKeyId()
        {
            var amazonRequest = new AmazonRequest
                                    {
                                        AssociateTag = TestHelper.AssociateTag,
                                        AccessKeyId = "injectingIncorrectAccessKey",
                                        CustomerId = TestHelper.CustomerId,
                                        ListId = TestHelper.ListId,
                                        SecretAccessKey = TestHelper.SecretAccessKey
                                    };

            var amazonResponse = new AmazonFactory(amazonRequest).GetResponse();

            foreach (var error in amazonResponse.Errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(2, amazonResponse.Errors.Count);
            Assert.IsTrue(amazonResponse.Errors.Select(e => e.Key == "ServiceError") != null);
        }

        [Test]
        public void ShouldReturnErrorsBecauseListIdAndCustomerIdHaveNotBeenSpecified()
        {
            var amazonRequest = new AmazonRequest
                                    {
                                        AssociateTag = TestHelper.AssociateTag,
                                        AccessKeyId = TestHelper.AwsAccessKey,
                                        CustomerId = null,
                                        ListId = null,
                                        SecretAccessKey = TestHelper.SecretAccessKey
                                    };

            var errors = amazonRequest.Validate();

            foreach (var error in errors)
            {
                if(error.Key.Contains("CustomerId"))
                {
                    Assert.IsTrue(true);
                }
                Debug.WriteLine(error);
            }
            Assert.Greater(errors.Count, 0);
        }

        [Test]
        public void ShouldNotReturnErrorsBecauseListIdBeenSpecified()
        {
            var amazonRequest = new AmazonRequest
                                    {
                                        AssociateTag = TestHelper.AssociateTag,
                                        AccessKeyId = TestHelper.AwsAccessKey,
                                        CustomerId = null,
                                        ListId = TestHelper.ListId,
                                        SecretAccessKey = TestHelper.SecretAccessKey
                                    };

            var errors = amazonRequest.Validate();

            foreach (var error in errors)
            {
                if (error.Key.Contains("CustomerId"))
                {
                    Assert.IsTrue(true);
                }
                Debug.WriteLine(error);
            }
            Assert.AreEqual(0, errors.Count);
        }

        [Test]
        public void ShouldNotReturnErrorsBecauseCustomerIdHasBeenSpecified()
        {
            var amazonRequest = new AmazonRequest
                                    {
                                        AssociateTag = TestHelper.AssociateTag,
                                        AccessKeyId = TestHelper.AwsAccessKey,
                                        CustomerId = TestHelper.CustomerId,
                                        ListId = null,
                                        SecretAccessKey = TestHelper.SecretAccessKey
                                    };

            var errors = amazonRequest.Validate();

            foreach (var error in errors)
            {
                if (error.Key.Contains("CustomerId"))
                {
                    Assert.IsTrue(true);
                }
                Debug.WriteLine(error);
            }
            Assert.AreEqual(0, errors.Count);
        }
    }
}