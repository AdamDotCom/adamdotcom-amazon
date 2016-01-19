# An Amazon Review and Wish List Consumer for .NET #

Get your Reviews and Wish List from Amazon then store it locally in XML format.

# Introduction #

To use this module for your own Amazon reviews and product lists you will need to sign-up for the following:
  * An Amazon Web Service (AWS) access key. http://aws.amazon.com/
  * An Amazon Associates tag. https://affiliate-program.amazon.com/gp/associates/join
  * An Amazon Account. http://www.amazon.com/gp/css/homepage.html

Then you will need to write a review or create a wish list, and then find the following:
  * The listid you want to retrieve a list from.
> _Don't know where to find this? I found my ListId by navigating to my wish list: http://www.amazon.com/gp/registry/registry.html/?id=3JU6ASKNUS7B8 then pulling out 3JU6ASKNUS7B8 (the identifier after id= from the url)._
  * The CustomerId you want to retrieve reviews from.
> _Don't know where to find this? I found my CustomerId by navigating to my reviews: http://www.amazon.com/gp/cdp/member-reviews/A2JM0EQJELFL69/ then pulling out A2JM0EQJELFL69 (the last element of the url)._

# Example #

```

//Hydrate the request object with your Amazon details
IAmazonRequest amazonRequest = new AmazonRequest();
amazonRequest.AssociateTag = "adamkahtavaap-20";
amazonRequest.AWSAccessKeyId = "1MRF________MR2";
amazonRequest.CustomerId = "A2JM0EQJELFL69";
amazonRequest.ListId = "3JU6ASKNUS7B8";

//Hydrate your file parameter object with your file details (where you want the files saved to)
IFileParameters fileParameters = new FileParameters();

string folderPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "Xml");
fileParameters.ProductFileNameAndPath = folderPath + @"\Products.xml";
fileParameters.ReviewFileNameAndPath = folderPath + @"\Reviews.xml";
fileParameters.ErrorFileNameAndPath = folderPath + @"\Errors.xml";

IAmazonApplication amazonApplication = new AmazonApplication(amazonRequest, fileParameters);

amazonApplication.Save();

```