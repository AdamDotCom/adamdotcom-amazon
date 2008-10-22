using System;
using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.com.amazon.webservices;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class ProductMapper : IProductMapper, IDisposable
    {
        private string awsAccessKeyId;
        private string associateTag;
        private List<string> errors;
        private AWSECommerceService awseCommerceService;

        public ProductMapper(string awsAccessKeyId, string associateTag)
        {
            awseCommerceService = new AWSECommerceService();
            this.awsAccessKeyId = awsAccessKeyId;
            this.associateTag = associateTag;

            errors = new List<string>();
        }

        public virtual List<string> GetErrors()
        {
            return errors;
        }

        public virtual List<ProductDTO> GetProducts(List<string> asinList)
        {
            List<ProductDTO> productsToReturn = new List<ProductDTO>();

            ItemSearchRequest[] allItemSearchRequests = new ItemSearchRequest[asinList.Count];

            for (int i = 0; i < asinList.Count; i++ )
            {
                allItemSearchRequests[i] = new ItemSearchRequest();
                allItemSearchRequests[i].SearchIndex = "Books";
                allItemSearchRequests[i].Power = "asin:" + asinList[i];
                allItemSearchRequests[i].ResponseGroup = new[] { "Small" };
            }

            //ItemSearchRequest can only be made in 2's
            ItemSearchRequest[] myItemSearchRequests = new ItemSearchRequest[2];

            bool isASINListOdd = (asinList.Count % 2 != 0);

            for (int i = 0; i < asinList.Count ; i++)
            {
                myItemSearchRequests[i%2] = allItemSearchRequests[i];

                if ((i != 0 && ((i + 1)%2 == 0)) || (isASINListOdd && asinList.Count == (i + 1)))
                {
                    ItemSearch itemSearch = new ItemSearch();
                    itemSearch.AWSAccessKeyId = awsAccessKeyId;
                    itemSearch.AssociateTag = associateTag;
                    itemSearch.Request = myItemSearchRequests;

                    ItemSearchResponse itemSearchResponse;

                    using (awseCommerceService)
                    {
                        itemSearchResponse = awseCommerceService.ItemSearch(itemSearch);
                    }

                    if (itemSearchResponse.Items == null)
                    {
                        if (itemSearchResponse.OperationRequest.Errors != null)
                        {
                            MapErrors(itemSearchResponse.OperationRequest.Errors);
                        }

                        break;
                    }

                    productsToReturn.AddRange(MapProducts(itemSearchResponse.Items));

                    myItemSearchRequests = new ItemSearchRequest[2];
                }
            }

            return productsToReturn;
        }

        private ProductDTO MapProduct(Item productItem)
        {
            var productToReturn = new ProductDTO()
            {
                ASIN = productItem.ASIN,
                Title = productItem.ItemAttributes.Title,
                Authors = productItem.ItemAttributes.Author,
                Url = productItem.DetailPageURL,
                Publisher = productItem.ItemAttributes.Manufacturer
            };

            return productToReturn;
        }

        private List<ProductDTO> MapProducts(Items[] productList)
        {
            var productsToReturn = new List<ProductDTO>();

            foreach (Items listItem in productList)
            {
                if(listItem.Item == null)
                {
                    continue;
                }

                productsToReturn.Add(MapProduct(listItem.Item[0]));
            }

            return productsToReturn;
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