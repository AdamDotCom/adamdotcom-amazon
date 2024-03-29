﻿using System.Diagnostics;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class ReviewMapperTest
    {
        private IReviewMapper reviewMapper;

        [TestFixtureSetUp]
        protected void SetUp()
        {
            reviewMapper = new ReviewMapper(TestHelper.AwsAccessKey, TestHelper.AssociateTag, TestHelper.SecretAccessKey, "A2JM0EQJELFL69");
        }

        [Test]
        public void ShouldBeAbleToGetReviewsFromAmazon()
        {
            var reviews = reviewMapper.GetReviews();

            Assert.AreNotEqual(0, reviews.Count);

            Assert.Greater(reviews.Count, 10);

            foreach (var review in reviews)
            {
                Assert.IsFalse((string.IsNullOrEmpty(review.ASIN)));
                Debug.WriteLine(review.ASIN);
            }
        }

        [Test]
        public void ShouldBeAbleToGetReviewsFromAmazonWithNoErrors()
        {
            reviewMapper.GetReviews();

            var errors = reviewMapper.GetErrors();

            foreach (var error in errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(0, errors.Count);
        }
    }
}