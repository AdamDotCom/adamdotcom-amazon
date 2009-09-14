using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace AdamDotCom.Amazon.Service
{
    public class ProfileSniffer
    {
        private const string profileSearchUri = "http://www.amazon.com/gp/pdp/search?ie=UTF8&flatten=1&keywords={0}";
        private string pageSource;

        public List<KeyValuePair<string, string>> Errors { get; set; }

        public ProfileSniffer()
        {
            Errors = new List<KeyValuePair<string, string>>();
        }

        public ProfileSniffer(string username)
        {
            Errors = new List<KeyValuePair<string, string>>();

            var webClient = new WebClient();

            try
            {
                pageSource = webClient.DownloadString(string.Format(profileSearchUri, username));
            }
            catch(Exception ex)
            {
                Errors.Add(new KeyValuePair<string, string>("ProfileSniffer", ex.Message));
            }
        }

        private void AddNotFoundError(string token)
        {
            Errors.Add(new KeyValuePair<string, string>(token, string.Format("{0} could not be found", token)));
        }

        private string GetTokenString(Regex wishlistRegex, string token)
        {
            if(string.IsNullOrEmpty(pageSource))
            {
                return null;
            }

            var match = wishlistRegex.Match(pageSource);

            if (match.Success)
            {
                try
                {
                    return match.Groups[token].ToString();
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public Profile GetProfile()
        {
            var wishlistRegex = new Regex("ref=cm_psrch_wishl?(.*)&id=(?<ListId>(.*))\" title=\"View Wish List\"");
            var listId = GetTokenString(wishlistRegex, "ListId");

            var reviewsRegex = new Regex("member-reviews/(?<CustomerId>(.*))/ref=cm_psrch_reviews");
            var customerId = GetTokenString(reviewsRegex, "CustomerId");

            if (string.IsNullOrEmpty(listId) || string.IsNullOrEmpty(customerId))
            {
                wishlistRegex = new Regex("/wishlist/(?<ListId>(.*))/ref=cm_pdp_wish_all_itms");
                listId = GetTokenString(wishlistRegex, "ListId");

                reviewsRegex = new Regex("profile/(?<CustomerId>(.*))/ref=cm_pdp_profile_tag_interesting");
                customerId = GetTokenString(reviewsRegex, "CustomerId");                
            }
            if (string.IsNullOrEmpty(listId))
            {
                AddNotFoundError("ListId");
            }
            if (string.IsNullOrEmpty(customerId))
            {
                AddNotFoundError("CustomerId");
            }

            return new Profile {CustomerId = customerId, ListId = listId};
        }
    }
}