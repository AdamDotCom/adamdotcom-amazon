using System.Collections.Generic;
using System.Diagnostics;
using AdamDotCom.Amazon.Application;
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

        [TestFixtureSetUp]
        protected void SetUp()
        {
        }

        [Test]
        public void ShouldMapFakeDTOsTogether()
        {
            List<ProductDTO> productDTOs = new List<ProductDTO>();

            for (int i = 0; i < 10; i++)
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.ASIN = i.ToString();

                productDTOs.Add(productDTO);
            }

            List<ReviewDTO> reviewDTOs = new List<ReviewDTO>();
            for (int i = 5; i < 15; i++)
            {
                ReviewDTO reviewDTO = new ReviewDTO();
                reviewDTO.ASIN = i.ToString();

                reviewDTOs.Add(reviewDTO);
            }

            IAmazonRequest amazonRequest = new AmazonRequest();

            reviewMapper = new ReviewListMapper(amazonRequest, reviewDTOs, productDTOs);

            IList<Review> reviews = reviewMapper.GetReviewList();

            Assert.AreNotEqual(0, reviews.Count);
            Debug.WriteLine(reviews.Count);
        }

        [Test]
        public void ShouldMapRealAmazonDataTogether()
        {
            IAmazonRequest amazonRequest = new AmazonRequest();
            amazonRequest.AssociateTag = "adamkahtavaap-20";
            amazonRequest.AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2";
            amazonRequest.CustomerId = "A2JM0EQJELFL69";

            reviewMapper = new ReviewListMapper(amazonRequest);

            IList<Review> reviews = reviewMapper.GetReviewList();

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