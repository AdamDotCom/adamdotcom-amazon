using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator.com.amazon.webservices;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class ListMapper : IListMapper
    {
        private string listId;
        private string awsAccessKeyId;
        private string associateTag;
        private IList<KeyValuePair<string, string>> errors;

        public ListMapper(string accessKeyId, string associateTag, string secretAccessKey, string listId)
        {
            AWSECommerceServiceInstance.SetPolicy(accessKeyId, secretAccessKey);

            this.listId = listId;
            this.awsAccessKeyId = accessKeyId;
            this.associateTag = associateTag;

            errors = new List<KeyValuePair<string, string>>();
        }

        public virtual IList<KeyValuePair<string, string>> GetErrors()
        {
            return errors;
        }

        public virtual List<ListItemDTO> GetList()
        {
            var listItemsToReturn = new List<ListItemDTO>(); 
            
            var listLookupResponse = new ListLookupResponse();

            //Amazon Lists can return 30 items at 10 items per page and 
            for (int currentPageRequest = 1; currentPageRequest <= 3; currentPageRequest++)
            {

                var listLookupRequest = new ListLookupRequest
                                            {
                                                ListId = listId,
                                                ListType = ListLookupRequestListType.WishList,
                                                ListTypeSpecified = true,
                                                ProductGroup = "Book",
                                                ResponseGroup = new[] {"ItemIds", "Small"},
                                                ProductPage = currentPageRequest.ToString()
                                            };

                var requests = new[] { listLookupRequest };

                var listLookup = new ListLookup
                                     {
                                         AWSAccessKeyId = awsAccessKeyId,
                                         AssociateTag = associateTag,
                                         Request = requests
                                     };

                listLookupResponse = AWSECommerceServiceInstance.AWSECommerceService.ListLookup(listLookup);

                if (listLookupResponse.Lists == null || listLookupResponse.Lists[0].List == null)
                {
                    if (listLookupResponse.OperationRequest.Errors != null)
                    {
                        MapErrors(listLookupResponse.OperationRequest.Errors);
                    }
                    if (listLookupResponse.Lists[0].Request.Errors != null)
                    {
                        MapErrors(listLookupResponse.Lists[0].Request.Errors);
                    }
                    break;
                }

                listItemsToReturn.AddRange(MapListItems(listLookupResponse.Lists[0].List[0].ListItem));
            }

            return listItemsToReturn;
        }

        private static ListItemDTO MapListItem(Item listItem)
        {
            var listItemToReturn = new ListItemDTO()
            {
                ASIN = listItem.ASIN 
            };

            return listItemToReturn;
        }

        private static List<ListItemDTO> MapListItems(IEnumerable<ListItem> listItems)
        {
            var listItemsToReturn = new List<ListItemDTO>();

            foreach (ListItem listItem in listItems)
            {
                listItemsToReturn.Add(MapListItem(listItem.Item));
            }

            return listItemsToReturn;
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