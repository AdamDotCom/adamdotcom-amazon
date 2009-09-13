using System.Diagnostics;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class ProductListMapperTest
    {
        IProductListMapper productListMapper;

        [Test]
        public void ShouldMapRealAmazonDataTogether()
        {
            var amazonRequest = TestHelper.ValidAmazonRequest;

            productListMapper = new ProductListMapper(amazonRequest);

            var products = productListMapper.GetList();

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
