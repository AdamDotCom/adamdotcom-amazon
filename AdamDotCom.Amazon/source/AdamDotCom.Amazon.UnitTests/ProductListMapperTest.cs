using System.Collections.Generic;
using System.Diagnostics;
using AdamDotCom.Amazon.Domain;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class ProductListMapperTest
    {
        IProductListMapper productListMapper;

        [TestFixtureSetUp]
        protected void SetUp()
        {
        }

        [Test]
        public void ShouldMapRealAmazonDataTogether()
        {
            IAmazonRequest amazonRequest = new AmazonRequest();
            amazonRequest.AssociateTag = "adamkahtavaap-20";
            amazonRequest.AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2";
            amazonRequest.ListId = "3JU6ASKNUS7B8";

            productListMapper = new ProductListMapper(amazonRequest);

            IList<Product> products = productListMapper.GetList();

            Assert.AreNotEqual(0, products.Count);

            Assert.IsNotNull(products[0].Authors);
            Assert.IsNotNull(products[0].AuthorsMLA);
            Assert.IsNotNull(products[0].ProductPreviewUrl);
            Assert.IsNotNull(products[0].ImageUrl);
            Assert.IsNotNull(products[0].Publisher);
            Debug.WriteLine(products.Count);
        }

    }
}
