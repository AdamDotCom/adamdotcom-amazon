using System.Collections.Generic;
using System.Diagnostics;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;
using AdamDotCom.Amazon.WebServiceTranslator;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class ReviewListMapperTest
    {
        IReviewListMapper reviewMapper;

        [Test]
        public void ShouldMapFakeDTOsTogether()
        {
            var productDTOs = new List<ProductDTO>();

            for (var i = 0; i < 10; i++)
            {
                var productDTO = new ProductDTO {ASIN = i.ToString()};

                productDTOs.Add(productDTO);
            }

            var reviewDTOs = new List<ReviewDTO>();
            for (int i = 5; i < 15; i++)
            {
                var reviewDTO = new ReviewDTO {ASIN = i.ToString()};

                reviewDTOs.Add(reviewDTO);
            }

            var amazonRequest = new AmazonRequest();

            reviewMapper = new ReviewListMapper(amazonRequest, reviewDTOs, productDTOs);

            var reviews = reviewMapper.GetReviewList();

            Assert.AreNotEqual(0, reviews.Count);
            Debug.WriteLine(reviews.Count);
        }

        [Test]
        public void ShouldMapRealAmazonDataTogether()
        {
            var amazonRequest = TestHelper.ValidAmazonRequest;

            reviewMapper = new ReviewListMapper(amazonRequest);

            var reviews = reviewMapper.GetReviewList();

            Assert.AreNotEqual(0, reviews.Count);

            Assert.IsNotNull(reviews[0].Authors);
            Assert.IsNotNull(reviews[0].AuthorsMLA);
            Assert.IsNotNull(reviews[0].ProductPreviewUrl);
            Assert.IsNotNull(reviews[0].ImageUrl);
            Assert.IsNotNull(reviews[0].Publisher);
            Debug.WriteLine(reviews.Count);
        }
    }
}