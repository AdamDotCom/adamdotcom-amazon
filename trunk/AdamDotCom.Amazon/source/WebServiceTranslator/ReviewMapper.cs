using System;
using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Infrastructure;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class ReviewMapper: IReviewMapper
    {
        private string awsAccessKeyId;
        private string associateTag;
        private List<KeyValuePair<string, string>> errors;
        private string customerId;

        public ReviewMapper(string accessKeyId, string associateTag, string secretAccessKey, string customerId)
        {
            AWSECommerceServiceInstance.SetCredentials(accessKeyId, secretAccessKey);

            this.awsAccessKeyId = accessKeyId;
            this.associateTag = associateTag;
            this.customerId = customerId;

            errors = new List<KeyValuePair<string, string>>();
        }

        public virtual List<KeyValuePair<string, string>> GetErrors()
        {
            return errors;
        }

        public virtual List<ReviewDTO> GetReviews()
        {
            var reviewsToReturn = new List<ReviewDTO>();

            int numberOfReviewPages = 1;

            for (int currentPageRequest = 1; currentPageRequest <= numberOfReviewPages; currentPageRequest++)
            {
                var customerContentLookupRequest = new CustomerContentLookupRequest
                                                       {
                                                           CustomerId = customerId,
                                                           ResponseGroup = new[] {"CustomerReviews"},
                                                           ReviewPage = currentPageRequest.ToString()
                                                       };

                var customerContentLookupRequests = new[] {customerContentLookupRequest};

                var customerContentLookup = new CustomerContentLookup
                                                {
                                                    AWSAccessKeyId = awsAccessKeyId,
                                                    AssociateTag = associateTag,
                                                    Request = customerContentLookupRequests
                                                };

                var customerContentLookupResponse = AWSECommerceServiceInstance.AWSECommerceService.CustomerContentLookup(customerContentLookup);

                if (customerContentLookupResponse.Customers == null ||
                    customerContentLookupResponse.Customers[0].Customer == null ||
                    customerContentLookupResponse.Customers[0].Customer[0].CustomerReviews == null)
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

                numberOfReviewPages = Convert.ToInt32(customerContentLookupResponse.Customers[0].Customer[0].CustomerReviews[0].TotalReviewPages);

                reviewsToReturn.AddRange(MapReviews(customerContentLookupResponse.Customers[0].Customer[0].CustomerReviews[0].Review));
            }

            return reviewsToReturn;
        }

        private static ReviewDTO MapReview(Review review)
        {
            var reviewToReturn = new ReviewDTO
                                     {
                                         ASIN = review.ASIN,
                                         Rating = review.Rating,
                                         Summary = review.Summary,
                                         Content = review.Content,
                                         Date = Convert.ToDateTime(review.Date),
                                         HelpfulVotes = Convert.ToInt32(review.HelpfulVotes),
                                         TotalVotes = Convert.ToInt32(review.TotalVotes),
                                     };

            return reviewToReturn;
        }

        private static List<ReviewDTO> MapReviews(IEnumerable<Review> reviews)
        {
            var reviewsToReturn = new List<ReviewDTO>();

            foreach (Review review in reviews)
            {
                reviewsToReturn.Add(MapReview(review));
            }

            return reviewsToReturn;
        }

        private void MapErrors(IEnumerable<ErrorsError> listErrors)
        {
            foreach (ErrorsError error in listErrors)
            {
                errors.Add(new KeyValuePair<string, string>(error.Code, error.Message));
            }
        }
    }
}