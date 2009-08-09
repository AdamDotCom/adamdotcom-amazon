using System.Text;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;

namespace AdamDotCom.Amazon.Domain.Extensions
{
    public static class FormatHelper
    {
        public static string Authors(this IProductDTO product)
        {
            string[] authors = product.Authors;
            if(authors == null)
            {
                return null;
            }

            string delimiter = ", ";

            StringBuilder authorsToReturn = new StringBuilder();

            foreach(string author in authors)
            {
                authorsToReturn.Append(author + delimiter);
            }

            return authorsToReturn.Remove(authorsToReturn.Length - delimiter.Length, delimiter.Length).ToString();
        }

        public static string AuthorsInMlaFormat(this IProductDTO product)
        {
            string[] authors = product.Authors;
            if (authors == null)
            {
                return null;
            }

            StringBuilder authorsMLA = new StringBuilder();

            authorsMLA.Append(OrderFirstLastAndMiddeNamesForMla(authors[0]));

            for (int i = 1; i < authors.Length; i++)
            {
                string authorName = authors[i];

                if (authors.Length == i + 1 && authors.Length <= 3)
                {
                    authorsMLA.Append(", and " + OrderFirstLastAndMiddeNamesForMla(authorName));
                }
                else if (authors.Length > 3)
                {
                    authorsMLA.Append(", et al");
                    break;
                }
                else
                {
                    authorsMLA.Append(", " + OrderFirstLastAndMiddeNamesForMla(authorName));
                }
            }
            
            authorsMLA.Append(".");

            return authorsMLA.ToString();
        }

        public static string OrderFirstLastAndMiddeNamesForMla(string authorName)
        {
            string delimiter = " ";

            string[] nameSplit = authorName.Split(new[] { ' ' });

            StringBuilder returnValue = new StringBuilder();

            for (int i = nameSplit.Length - 1; i >= 0; i--)
            {
                returnValue.Append(nameSplit[i]);
                returnValue.Append(delimiter);
            }

            return returnValue.ToString().Remove(returnValue.Length - delimiter.Length, delimiter.Length);
        }
    }
}