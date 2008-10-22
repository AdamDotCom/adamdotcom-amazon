using System;
using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator.com.amazon.webservices;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class ListMapper : IListMapper, IDisposable
    {
        private string listId;
        private string awsAccessKeyId;
        private string associateTag;
        private List<string> errors;
        private AWSECommerceService awseCommerceService;

        public ListMapper(string awsAccessKeyId, string associateTag, string listId)
        {
            awseCommerceService = new AWSECommerceService();
            this.listId = listId;
            this.awsAccessKeyId = awsAccessKeyId;
            this.associateTag = associateTag;

            errors = new List<string>();
        }

        public virtual List<string> GetErrors()
        {
            return errors;
        }

        public virtual List<ListItemDTO> GetList()
        {
            List<ListItemDTO> listItemsToReturn = new List<ListItemDTO>(); 
            
            ListLookupResponse listLookupResponse = new ListLookupResponse();

            //Amazon Lists can return 300 items at 10 items per page and 
            // I can't figure out how to find the total number of pages number in the Amazon Response
            // so I'm using the max page requests just incase.
            for (int currentPageRequest = 1; currentPageRequest <= 30; currentPageRequest++)
            {

                ListLookupRequest listLookupRequest = new ListLookupRequest();
                listLookupRequest.ListId = listId;
                listLookupRequest.ListType = ListLookupRequestListType.WishList;
                listLookupRequest.ListTypeSpecified = true;
                listLookupRequest.ProductGroup = "Book";
                listLookupRequest.ResponseGroup = new[] { "ItemIds", "Small" };

                listLookupRequest.ProductPage = currentPageRequest.ToString();

                var requests = new[] { listLookupRequest };

                ListLookup listLookup = new ListLookup();
                listLookup.AWSAccessKeyId = awsAccessKeyId;
                listLookup.AssociateTag = associateTag;
                listLookup.Request = requests;

                using (awseCommerceService)
                {
                    listLookupResponse = awseCommerceService.ListLookup(listLookup);
                }

                if (listLookupResponse.Lists == null || listLookupResponse.Lists[0].List == null)
                {
                    if (listLookupResponse.OperationRequest.Errors != null)
                    {
                        MapErrors(listLookupResponse.OperationRequest.Errors);
                    }
                    break;
                }

                listItemsToReturn.AddRange(MapListItems(listLookupResponse.Lists[0].List[0].ListItem));
            }

            return listItemsToReturn;
        }

        private ListItemDTO MapListItem(Item listItem)
        {
            var listItemToReturn = new ListItemDTO()
            {
                ASIN = listItem.ASIN 
            };

            return listItemToReturn;
        }

        private List<ListItemDTO> MapListItems(ListItem[] listItems)
        {
            var listItemsToReturn = new List<ListItemDTO>();

            foreach (ListItem listItem in listItems)
            {
                listItemsToReturn.Add(MapListItem(listItem.Item));
            }

            return listItemsToReturn;
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