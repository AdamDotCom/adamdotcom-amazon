using System.Collections.Generic;
using System.Diagnostics;
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
            reviewMapper = new ReviewMapper("1MRFMGASE6CQKS2WTMR2", "adamkahtavaap-20", "A2JM0EQJELFL69");
        }

        [Test]
        public void ShouldBeAbleToGetReviewsFromAmazon()
        {
            IList<ReviewDTO> reviews = reviewMapper.GetReviews();

            Assert.AreNotEqual(0, reviews.Count);

            Assert.Greater(reviews.Count, 10);

            foreach (IReviewDTO review in reviews)
            {
                Assert.IsFalse((string.IsNullOrEmpty(review.ASIN)));
                Debug.WriteLine(review.ASIN);
            }
        }

        [Test]
        public void ShouldBeAbleToGetReviewsFromAmazonWithNoErrors()
        {
            reviewMapper.GetReviews();

            IList<string> errors = reviewMapper.GetErrors();

            foreach (string error in errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(0, errors.Count);
        }

    }
}