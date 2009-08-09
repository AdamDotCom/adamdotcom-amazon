using System;
using System.IO;
using System.Web;
using AdamDotCom.Amazon.Application;
using AdamDotCom.Amazon.Application.Interfaces;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;

namespace SampleWebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["LoadReviewsAndWishListFromAmazon"]))
            {
                LoadReviewsAndWishListFromAmazon();
            }
        }

        private bool LoadReviewsAndWishListFromAmazon()
        {
            try
            {
                IAmazonRequest amazonRequest = new AmazonRequest();
                amazonRequest.AssociateTag = "adamkahtavaap-20";
                amazonRequest.AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2";
                amazonRequest.CustomerId = "A2JM0EQJELFL69";
                amazonRequest.ListId = "3JU6ASKNUS7B8";

                IFileParameters fileParameters = new FileParameters();

                string folderPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "Xml");
                fileParameters.ProductFileNameAndPath = folderPath + @"\Products.xml";
                fileParameters.ReviewFileNameAndPath = folderPath + @"\Reviews.xml";
                fileParameters.ErrorFileNameAndPath = folderPath + @"\Errors.xml";

                IAmazonApplication amazonApplication = new AmazonApplication(amazonRequest, fileParameters);

                return amazonApplication.Save();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
