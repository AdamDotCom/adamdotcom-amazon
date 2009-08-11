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
        private List<string> errors;

        public ReviewListMapper(IAmazonRequest amazonRequest, List<ReviewDTO> reviews, List<ProductDTO> products)
        {
            this.amazonRequest = amazonRequest;

            this.reviews = reviews;
            this.products = products;
            errors = new List<string>();
        }

        public ReviewListMapper(IAmazonRequest amazonRequest)
        {
            this.amazonRequest = amazonRequest;
            errors = new List<string>();

            try
            {
                var reviewMapper = new ReviewMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag, amazonRequest.CustomerId);

                using (reviewMapper)
                {
                    reviews = reviewMapper.GetReviews();

                    foreach (var error in reviewMapper.GetErrors())
                    {
                        errors.Add(error);
                    }

                }

                var productMapper = new ProductMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag);

                using (productMapper)
                {
                    products = productMapper.GetProducts(reviews.ConvertAll(review => review.ASIN));

                    foreach (var error in productMapper.GetErrors())
                    {
                        errors.Add(error);
                    }
                }
            }
            catch(Exception ex)
            {
                errors.Add(ex.Message);
            }
        }

        public virtual List<Review> GetReviewList()
        {
            return MapProductsAndReviews(products, reviews);
        }

        public virtual List<string> GetErrors()
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