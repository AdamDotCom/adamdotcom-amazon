using System;
using System.Collections.Generic;
using System.Linq;
using AdamDotCom.Amazon.Domain.Extensions;
using AdamDotCom.Amazon.Domain.Interfaces;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;

namespace AdamDotCom.Amazon.Domain
{
    public class ReviewListMapper: IReviewListMapper
    {
        private IAmazonRequest amazonRequest;
        private List<ReviewDTO> reviews;
        private List<ProductDTO> products;
        private IList<KeyValuePair<string, string>> errors;

        public ReviewListMapper(IAmazonRequest amazonRequest, List<ReviewDTO> reviews, List<ProductDTO> products)
        {
            this.amazonRequest = amazonRequest;

            this.reviews = reviews;
            this.products = products;
            errors = new List<KeyValuePair<string, string>>();
        }

        public ReviewListMapper(IAmazonRequest amazonRequest)
        {
            this.amazonRequest = amazonRequest;
            errors = new List<KeyValuePair<string, string>>();

            try
            {
                var reviewMapper = new ReviewMapper(amazonRequest.AccessKeyId, amazonRequest.AssociateTag, amazonRequest.SecretAccessKey, amazonRequest.CustomerId);

                reviews = reviewMapper.GetReviews();

                foreach (var error in reviewMapper.GetErrors())
                {
                    errors.Add(error);
                }

                var productMapper = new ProductMapper(amazonRequest.AccessKeyId, amazonRequest.AssociateTag, amazonRequest.SecretAccessKey);

                products = productMapper.GetProducts(reviews.ConvertAll(review => review.ASIN));

                foreach (var error in productMapper.GetErrors())
                {
                    errors.Add(error);
                }
            }
            catch(Exception ex)
            {
                errors.Add(new KeyValuePair<string, string>("ServiceError", ex.Message));
            }
        }

        public virtual List<Review> GetReviewList()
        {
            return MapProductsAndReviews(products, reviews);
        }

        public virtual IList<KeyValuePair<string, string>> GetErrors()
        {
            return errors;
        }

        private Review MapProductAndReview(IProductDTO product, IReviewDTO review)
        {
            var reviewToReturn = new Review
                                     {
                                         ASIN = product.ASIN,
                                         Authors = product.Authors(),
                                         AuthorsMLA = product.AuthorsInMlaFormat(),
                                         ImageUrl = product.ProductImageUrl(amazonRequest.AssociateTag),
                                         ProductPreviewUrl = product.ProductPreviewUrl(amazonRequest.AssociateTag),
                                         Publisher = product.Publisher,
                                         Title = product.Title,
                                         Url = product.Url,
                                         Content = review.Content,
                                         Date = review.Date,
                                         HelpfulVotes = review.HelpfulVotes,
                                         Rating = review.Rating,
                                         Summary = review.Summary,
                                         TotalVotes = review.TotalVotes
                                     };

            return reviewToReturn;
        }

        private List<Review> MapProductsAndReviews(IEnumerable<ProductDTO> products, IEnumerable<ReviewDTO> reviews)
        {
            var results = from product in products
                                          join review in reviews on product.ASIN equals review.ASIN
                                          select MapProductAndReview(product, review);

            return results.ToList().ConvertAll(review => review);
        }
    }
}