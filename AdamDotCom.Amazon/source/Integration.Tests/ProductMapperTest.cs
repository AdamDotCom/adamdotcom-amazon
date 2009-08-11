using System.Collections.Generic;
using System.Diagnostics;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class ProductMapperTest
    {
        private IProductMapper productMapper;
        private List<string> ASINList;

        [TestFixtureSetUp]
        protected void SetUp()
        {
            productMapper = new ProductMapper("1MRFMGASE6CQKS2WTMR2", "adamkahtavaap-20");

            ASINList = new List<string>
                           {
                               "1556159005",
                               "0201485672",
                               "0471137723",
                               "0132624788",
                               "0471467413",
                               "0735618798",
                               "1886411972",
                               "0131495054",
                               "0673386023",
                               "1883577039",
                               "0262560992",
                               "026256100X",
                               "0262562146"
                           };
        }

        [Test]
        public void ShouldBeAbleToGetProductsFromAmazon()
        {
            var products = productMapper.GetProducts(ASINList);

            Assert.AreEqual(ASINList.Count, products.Count);

            Debug.WriteLine(products.Count);

            foreach (var product in products)
            {
                Assert.IsFalse((string.IsNullOrEmpty(product.ASIN)));
                Assert.IsNotNull(product.Authors);
                Assert.IsNotNull(product.Publisher);
                Debug.WriteLine(product.ASIN);
            }

            Assert.IsNotNull(products[0].Authors);

        }

        [Test]
        public void ShouldBeAbleToGetProductsFromAmazonWithNoErrors()
        {
            productMapper.GetProducts(ASINList);

            var errors = productMapper.GetErrors();

            foreach (var error in errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(0, errors.Count);
        }
    }
}