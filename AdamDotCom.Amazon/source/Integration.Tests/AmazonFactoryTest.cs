using System.Diagnostics;
using System.Linq;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class AmazonFactoryTest
    {
        private IAmazonRequest amazonRequest;

        [TestFixtureSetUp]
        protected void SetUp()
        {
            amazonRequest = new AmazonRequest
                                {
                                    AssociateTag = "adamkahtavaap-20",
                                    AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2",
                                    CustomerId = "A2JM0EQJELFL69",
                                    ListId = "3JU6ASKNUS7B8"
                                };
        }

        [Test]
        public void ShouldInvokeAndReturnSuccessfulProductsAndReviewsWithNoErrors()
        {
            amazonRequest.AssociateTag = "adamkahtavaap-20";
            amazonRequest.AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2";
            amazonRequest.CustomerId = "A2JM0EQJELFL69";
            amazonRequest.ListId = "3JU6ASKNUS7B8";

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
        public void ShouldInvokeAndReturnErrorsFromIncorrectCustomerIdAndListId()
        {
            var amazonRequestLocal = amazonRequest;

            amazonRequestLocal.CustomerId = "injectingIncorrectData";
            amazonRequestLocal.ListId = "injectingIncorrectData2";

            var amazonResponse = new AmazonFactory(amazonRequestLocal).GetResponse();

            Assert.AreEqual(2, amazonResponse.Errors.Count);

            Assert.IsTrue(amazonResponse.Errors.Select(e => e.Key == "CustomerId") != null);
            Assert.IsTrue(amazonResponse.Errors.Select(e => e.Key == "ListId") != null);

            foreach (var error in amazonResponse.Errors)
            {
                Debug.WriteLine(error);
            }

        }

        [Test]
        public void ShouldInvokeAndReturnErrorsFromIncorrectAWSAccessKeyId()
        {
            var amazonRequestLocal = amazonRequest;
            amazonRequest.AWSAccessKeyId = "trash";

            var amazonResponse = new AmazonFactory(amazonRequestLocal).GetResponse();

            Assert.Greater(amazonResponse.Errors.Count, 0);

            foreach (var error in amazonResponse.Errors)
            {
                Debug.WriteLine(error);
            }
        }

        [Test]
        public void ShouldReturnErrorsBecauseListIdAndCustomerIdHaveNotBeenSpecified()
        {
            var amazonRequestLocal = new AmazonRequest
                                         {
                                             AssociateTag = "adamkahtavaap-20",
                                             AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2",
                                             CustomerId = string.Empty,
                                             ListId = string.Empty
                                         };

            var amazonResponse = new AmazonFactory(amazonRequestLocal).GetResponse();

            Assert.Greater(amazonResponse.Errors.Count, 0);

            foreach (var error in amazonResponse.Errors)
            {
                if(error.Key.Contains("CustomerId"))
                {
                    Assert.IsTrue(true);
                }
                Debug.WriteLine(error);
            }
        }

        [Test]
        public void ShouldNotReturnErrorsBecauseListIdBeenSpecified()
        {
            var amazonRequestLocal = new AmazonRequest
            {
                AssociateTag = "adamkahtavaap-20",
                AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2",
                CustomerId = null,
                ListId = "3JU6ASKNUS7B8"
            };

            var amazonResponse = new AmazonFactory(amazonRequestLocal).GetResponse();

            Assert.AreEqual(0, amazonResponse.Errors.Count);

            foreach (var error in amazonResponse.Errors)
            {
                if (error.Key.Contains("CustomerId"))
                {
                    Assert.IsTrue(true);
                }
                Debug.WriteLine(error);
            }
        }

        [Test]
        public void ShouldNotReturnErrorsBecauseCutsomerIdHasBeenSpecified()
        {
            var amazonRequestLocal = new AmazonRequest
            {
                AssociateTag = "adamkahtavaap-20",
                AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2",
                CustomerId = "A2JM0EQJELFL69",
                ListId = null
            };

            var amazonResponse = new AmazonFactory(amazonRequestLocal).GetResponse();

            Assert.AreEqual(0, amazonResponse.Errors.Count);

            foreach (var error in amazonResponse.Errors)
            {
                if (error.Key.Contains("CustomerId"))
                {
                    Assert.IsTrue(true);
                }
                Debug.WriteLine(error);
            }
        }

    }
}