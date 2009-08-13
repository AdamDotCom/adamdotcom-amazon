﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;

[assembly: ContractNamespace("http://adam.kahtava.com/services/amazon", ClrNamespace = "AdamDotCom.Amazon.Domain")]
namespace AdamDotCom.Amazon.Domain
{
    public class AmazonFactory : IAmazonFactory
    {
        private readonly ProductListMapper productListMapper;
        private readonly ReviewListMapper reviewListMapper;
        private readonly IAmazonRequest amazonRequest;

        public AmazonFactory(IAmazonRequest amazonRequest)
        {
            this.amazonRequest = amazonRequest;

            if (!string.IsNullOrEmpty(amazonRequest.ListId))
            {
                productListMapper = new ProductListMapper(amazonRequest);
            }

            if (!string.IsNullOrEmpty(amazonRequest.CustomerId))
            {
                reviewListMapper = new ReviewListMapper(amazonRequest);
            }
        }

        public AmazonResponse GetResponse()
        {
            var amazonResponse = new AmazonResponse {Errors = new List<string>()};

            if(string.IsNullOrEmpty(amazonRequest.CustomerId) && string.IsNullOrEmpty(amazonRequest.ListId))
            {
                amazonResponse.Errors.Add("A CustomerId or a ListId must be specified.");
            }

            if (productListMapper != null)
            {
                if (productListMapper.GetErrors().Count != 0)
                {
                    amazonResponse.Errors.AddRange(productListMapper.GetErrors());
                }
                else
                {
                    amazonResponse.Products = productListMapper.GetList();
                }
            }

            if (reviewListMapper != null)
            {
                if (reviewListMapper.GetErrors().Count != 0)
                {
                    amazonResponse.Errors.AddRange(reviewListMapper.GetErrors());
                }
                else
                {
                    amazonResponse.Reviews = reviewListMapper.GetReviewList();
                }
            }

            return amazonResponse;
        }
    }
}