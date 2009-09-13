using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.com.amazon.webservices;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class ProductMapper : IProductMapper
    {
        private string awsAccessKeyId;
        private string associateTag;
        private List<KeyValuePair<string, string>> errors;

        public ProductMapper(string accessKeyId, string associateTag, string secretAccessKey)
        {
            AWSECommerceServiceInstance.SetPolicy(accessKeyId, secretAccessKey);

            this.awsAccessKeyId = accessKeyId;
            this.associateTag = associateTag;

            errors = new List<KeyValuePair<string, string>>();
        }

        public virtual List<KeyValuePair<string, string>> GetErrors()
        {
            return errors;
        }

        public virtual List<ProductDTO> GetProducts(List<string> asinList)
        {
            var productsToReturn = new List<ProductDTO>();

            var allItemSearchRequests = new ItemSearchRequest[asinList.Count];

            for (int i = 0; i < asinList.Count; i++ )
            {
                allItemSearchRequests[i] = new ItemSearchRequest
                                               {
                                                   SearchIndex = "Books",
                                                   Power = ("asin:" + asinList[i]),
                                                   ResponseGroup = new[] {"Small"}
                                               };
            }

            //ItemSearchRequest can only be made in 2's
            var myItemSearchRequests = new ItemSearchRequest[2];

            bool isASINListOdd = (asinList.Count % 2 != 0);

            for (int i = 0; i < asinList.Count ; i++)
            {
                myItemSearchRequests[i%2] = allItemSearchRequests[i];

                if ((i != 0 && ((i + 1)%2 == 0)) || (isASINListOdd && asinList.Count == (i + 1)))
                {
                    var itemSearch = new ItemSearch
                                         {
                                             AWSAccessKeyId = awsAccessKeyId,
                                             AssociateTag = associateTag,
                                             Request = myItemSearchRequests
                                         };

                    var itemSearchResponse = AWSECommerceServiceInstance.AWSECommerceService.ItemSearch(itemSearch);

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
            var productToReturn = new ProductDTO
                                      {
                                          ASIN = productItem.ASIN,
                                          Title = productItem.ItemAttributes.Title,
                                          Authors = productItem.ItemAttributes.Author,
                                          Url = productItem.DetailPageURL,
                                          Publisher = productItem.ItemAttributes.Manufacturer
                                      };

            return productToReturn;
        }

        private List<ProductDTO> MapProducts(IEnumerable<Items> productList)
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

        private void MapErrors(IEnumerable<ErrorsError> listErrors)
        {
            foreach (ErrorsError error in listErrors)
            {
                errors.Add(new KeyValuePair<string, string>(error.Code, error.Message));
            }
        }
    }
}