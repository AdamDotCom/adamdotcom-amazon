using System;
using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.com.amazon.webservices;

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

        public List<string> GetErrors()
        {
            return errors;
        }

        public List<ProductDTO> GetProducts(List<string> asinList)
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

                    foreach (ProductDTO product in MapProducts(itemSearchResponse.Items))
                    {
                        productsToReturn.Add(product);
                    }

                    myItemSearchRequests = new ItemSearchRequest[2];
                }
            }

            return productsToReturn;
        }

        private IProductDTO MapProduct(Item productItem)
        {
            IProductDTO productToReturn = new ProductDTO();

            productToReturn.ASIN = productItem.ASIN;
            productToReturn.Title = productItem.ItemAttributes.Title;
            productToReturn.Authors = productItem.ItemAttributes.Author;
            productToReturn.Url = productItem.DetailPageURL;
            productToReturn.Publisher = productItem.ItemAttributes.Manufacturer;

            return productToReturn;
        }

        private List<IProductDTO> MapProducts(Items[] productList)
        {
            List<IProductDTO> productsToReturn = new List<IProductDTO>();

            foreach (Items listItem in productList)
            {
                if(listItem.Item == null)
                {
                    continue;
                }
                IProductDTO productToReturn = MapProduct(listItem.Item[0]);

                productsToReturn.Add(productToReturn);
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