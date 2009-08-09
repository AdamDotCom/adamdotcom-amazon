﻿using System.Diagnostics;
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
            amazonRequest = new AmazonRequest();
            amazonRequest.AssociateTag = "adamkahtavaap-20";
            amazonRequest.AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2";
            amazonRequest.CustomerId = "A2JM0EQJELFL69";
            amazonRequest.ListId = "3JU6ASKNUS7B8";
        }

        [Test]
        public void ShouldInvokeAndReturnSuccessfulProductsAndReviewsWithNoErrors()
        {
            amazonRequest.AssociateTag = "adamkahtavaap-20";
            amazonRequest.AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2";
            amazonRequest.CustomerId = "A2JM0EQJELFL69";
            amazonRequest.ListId = "3JU6ASKNUS7B8";

            IAmazonResponse amazonResponse = new AmazonFactory(amazonRequest).GetResponse();

            string errors = string.Empty;
            foreach (string error in amazonResponse.Errors)
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
            IAmazonRequest amazonRequestLocal = amazonRequest;

            amazonRequestLocal.CustomerId = "injectingIncorrectData";
            amazonRequestLocal.ListId = "injectingIncorrectData2";

            IAmazonResponse amazonResponse = new AmazonFactory(amazonRequestLocal).GetResponse();

            Assert.Greater(amazonResponse.Errors.Count, 0);

            foreach (string error in amazonResponse.Errors)
            {
                Debug.WriteLine(error);
            }

        }

        [Test]
        public void ShouldInvokeAndReturnErrorsFromIncorrectAWSAccessKeyId()
        {
            IAmazonRequest amazonRequestLocal = amazonRequest;
            amazonRequest.AWSAccessKeyId = "trash";

            IAmazonResponse amazonResponse = new AmazonFactory(amazonRequestLocal).GetResponse();

            Assert.Greater(amazonResponse.Errors.Count, 0);

            foreach (string error in amazonResponse.Errors)
            {
                Debug.WriteLine(error);
            }
        }

        [Test]
        public void ShouldReturnErrorsBecauseListIdAndCustomerIdHaveNotBeenSpecified()
        {
            IAmazonRequest amazonRequestLocal = new AmazonRequest();
            amazonRequestLocal.AssociateTag = "adamkahtavaap-20";
            amazonRequestLocal.AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2";
            amazonRequestLocal.CustomerId = string.Empty;
            amazonRequestLocal.ListId = string.Empty;

            IAmazonResponse amazonResponse = new AmazonFactory(amazonRequestLocal).GetResponse();

            Assert.Greater(amazonResponse.Errors.Count, 0);

            foreach (string error in amazonResponse.Errors)
            {
                if(error.Contains("CustomerId"))
                {
                    Assert.IsTrue(true);
                }
                Debug.WriteLine(error);
            }
        }

    }
}