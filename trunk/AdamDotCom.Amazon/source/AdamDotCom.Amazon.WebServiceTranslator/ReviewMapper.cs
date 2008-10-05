using System;
using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator.com.amazon.webservices;
using AdamDotCom.Amazon.WebServiceTranslator;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class ReviewMapper: IReviewMapper, IDisposable
    {
        private string awsAccessKeyId;
        private string associateTag;
        private List<string> errors;
        private AWSECommerceService awseCommerceService;
        private string customerId;

        public ReviewMapper(string awsAccessKeyId, string associateTag, string customerId)
        {
            awseCommerceService = new AWSECommerceService();
            this.awsAccessKeyId = awsAccessKeyId;
            this.associateTag = associateTag;
            this.customerId = customerId;

            errors = new List<string>();
        }

        public virtual List<string> GetErrors()
        {
            return errors;
        }

        public virtual List<ReviewDTO> GetReviews()
        {
            List<ReviewDTO> reviewsToReturn = new List<ReviewDTO>();

            int numberOfReviewPages = 1;

            for (int currentPageRequest = 1; currentPageRequest <= numberOfReviewPages; currentPageRequest++)
            {

                CustomerContentLookupRequest customerContentLookupRequest = new CustomerContentLookupRequest();

                customerContentLookupRequest.CustomerId = customerId;
                customerContentLookupRequest.ResponseGroup = new[] {"CustomerReviews"};
                customerContentLookupRequest.ReviewPage = currentPageRequest.ToString();

                CustomerContentLookupRequest[] customerContentLookupRequests = new[] {customerContentLookupRequest};

                CustomerContentLookup customerContentLookup = new CustomerContentLookup();
                customerContentLookup.AWSAccessKeyId = awsAccessKeyId;
                customerContentLookup.AssociateTag = associateTag;
                customerContentLookup.Request = customerContentLookupRequests;

                CustomerContentLookupResponse customerContentLookupResponse;
                using (awseCommerceService)
                {
                    customerContentLookupResponse = awseCommerceService.CustomerContentLookup(customerContentLookup);
                }

                if (customerContentLookupResponse.Customers == null || customerContentLookupResponse.Customers[0].Customer == null)
                {
                    if (customerContentLookupResponse.OperationRequest.Errors != null)
                    {
                        MapErrors(customerContentLookupResponse.OperationRequest.Errors);
                    }
                    else if (customerContentLookupResponse.Customers[0].Request.Errors != null )
                    {
                        MapErrors(customerContentLookupResponse.Customers[0].Request.Errors);
                    }
                    break;
                }

                numberOfReviewPages =
                    Convert.ToInt32(
                        customerContentLookupResponse.Customers[0].Customer[0].CustomerReviews[0].TotalReviewPages);

                foreach (ReviewDTO review in MapReviews(customerContentLookupResponse.Customers[0].Customer[0].CustomerReviews[0].Review))
                {
                    reviewsToReturn.Add(review);
                }
            }

            return reviewsToReturn;
        }

        private ReviewDTO MapReview(Review review)
        {
            ReviewDTO reviewToReturn = new ReviewDTO();

            reviewToReturn.ASIN = review.ASIN;
            reviewToReturn.Rating = review.Rating;
            reviewToReturn.Summary = review.Summary;
            reviewToReturn.Content = review.Content;
            reviewToReturn.Date = Convert.ToDateTime(review.Date);
            reviewToReturn.HelpfulVotes = Convert.ToInt32(review.HelpfulVotes);
            reviewToReturn.TotalVotes = Convert.ToInt32(review.TotalVotes);
            reviewToReturn.Content = review.Content;

            return reviewToReturn;
        }

        private List<ReviewDTO> MapReviews(Review[] reviews)
        {
            List<ReviewDTO> reviewsToReturn = new List<ReviewDTO>();

            foreach (Review review in reviews)
            {
                ReviewDTO reviewToReturn = MapReview(review);

                reviewsToReturn.Add(reviewToReturn);
            }

            return reviewsToReturn;
        }

        private void MapErrors(ErrorsError[] listErrors)
        {
            foreach (ErrorsError error in listErrors)
            {
                errors.Add(error.Code + " " + error.Message);
            }
        }

        public void Dispose()
        {
            awseCommerceService.Dispose();
        }
    }
}