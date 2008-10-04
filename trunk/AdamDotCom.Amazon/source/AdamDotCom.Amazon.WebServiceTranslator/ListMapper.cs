﻿using System;
using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator.com.amazon.webservices;
using AdamDotCom.Amazon.WebServiceTranslator;

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

        public List<string> GetErrors()
        {
            return errors;
        }

        public List<ListItemDTO> GetList()
        {
            List<ListItemDTO> listItemsToReturn = new List<ListItemDTO>(); 
            
            ListLookupResponse listLookupResponse = new ListLookupResponse();

            //Amazon Lists can return 300 items at 10 items per page
            for(int currentPageRequest = 1 ; currentPageRequest <= 30 ; currentPageRequest++)
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
                    else if (listLookupResponse.Lists[0].Request.Errors != null)
                    {
                        MapErrors(listLookupResponse.Lists[0].Request.Errors);
                    }
                    break;
                }              

                foreach (ListItemDTO wishListItem in MapListItems(listLookupResponse.Lists[0].List[0].ListItem))
                {
                    listItemsToReturn.Add(wishListItem);
                }
            }

            return listItemsToReturn;
        }

        private IListItemDTO MapListItem(Item listItem)
        {
            IListItemDTO listItemToReturn = new ListItemDTO();

            listItemToReturn.ASIN = listItem.ASIN;

            return listItemToReturn;
        }

        private List<IListItemDTO> MapListItems(ListItem[] listItems)
        {
            List<IListItemDTO> listItemsToReturn = new List<IListItemDTO>();

            foreach (ListItem listItem in listItems)
            {
                IListItemDTO listItemToReturn = MapListItem(listItem.Item);

                listItemsToReturn.Add(listItemToReturn);
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