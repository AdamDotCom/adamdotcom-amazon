using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator;

namespace AdamDotCom.Amazon.Domain
{
    public class ReviewListMapper: IReviewListMapper
    {
        private IAmazonRequest amazonRequest;
        private IList<ReviewDTO> reviews;
        private IList<ProductDTO> products;
        private List<string> errors;

        public ReviewListMapper(IAmazonRequest amazonRequest, IList<ReviewDTO> reviews, IList<ProductDTO> products)
        {
            this.amazonRequest = amazonRequest;

            this.reviews = reviews;
            this.products = products;
        }

        public ReviewListMapper(IAmazonRequest amazonRequest)
        {
            this.amazonRequest = amazonRequest;

            ReviewMapper reviewMapper = new ReviewMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag,
                                                         amazonRequest.CustomerId);
            reviews = reviewMapper.GetReviews();

            errors = reviewMapper.GetErrors();

            List<string> ASINList = new List<string>();
            foreach (ReviewDTO review in reviews)
            {
                ASINList.Add(review.ASIN);
            }

            ProductMapper productMapper = new ProductMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag);
            products = productMapper.GetProducts(ASINList);

            foreach (string error in productMapper.GetErrors())
            {
                errors.Add(error);
            }
        }

        public virtual List<Review> GetReviewList()
        {
            return MergeProductsWithReviews(products, reviews);
        }

        public virtual List<string> GetErrors()
        {
            return errors;
        }

        private Review MergeProductWithReview(ProductDTO product, ReviewDTO review)
        {
            Review reviewToReturn = new Review();

            reviewToReturn.ASIN = product.ASIN;
            reviewToReturn.Authors = FormatHelper.MapAuthors(product.Authors);
            reviewToReturn.AuthorsMLA = FormatHelper.MapAuthorsInMlaFormat(product.Authors);
            reviewToReturn.Content = review.Content;
            reviewToReturn.Date = review.Date;
            reviewToReturn.HelpfulVotes = review.HelpfulVotes;
            reviewToReturn.ImageUrl = AmazonAssetHelper.ProductImageUrl(amazonRequest.AssociateTag, product.ASIN);
            reviewToReturn.ProductPreviewUrl = AmazonAssetHelper.ProductPreviewUrl(amazonRequest.AssociateTag, product.ASIN);
            reviewToReturn.Publisher = product.Publisher;
            reviewToReturn.Rating = review.Rating;
            reviewToReturn.Summary = review.Summary;
            reviewToReturn.Title = product.Title;
            reviewToReturn.TotalVotes = review.TotalVotes;
            reviewToReturn.Url = product.Url;

            return reviewToReturn;
        }

        private List<Review> MergeProductsWithReviews(IList<ProductDTO> products, IList<ReviewDTO> reviews)
        {
            List<Review> reviewsToReturn = new List<Review>();

            foreach(ReviewDTO review in reviews)
            {
                foreach(ProductDTO product in products)
                {
                    if (review.ASIN == product.ASIN)
                    {
                        reviewsToReturn.Add(MergeProductWithReview(product, review));
                    }
                }
            }

            return reviewsToReturn;
        }
    }
}