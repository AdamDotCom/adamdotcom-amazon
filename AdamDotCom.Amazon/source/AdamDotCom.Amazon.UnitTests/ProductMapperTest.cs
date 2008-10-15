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

        [TestFixtureSetUp]
        protected void SetUp()
        {
            productMapper = new ProductMapper("1MRFMGASE6CQKS2WTMR2", "adamkahtavaap-20");
        }

        [Test]
        public void ShouldBeAbleToGetProductsFromAmazon()
        {
            List<string> ASINList = new List<string>();

            ASINList.Add("1556159005");
            ASINList.Add("0201485672");
            ASINList.Add("0471137723");
            ASINList.Add("0132624788");
            ASINList.Add("0471467413");
            ASINList.Add("0735618798");
            ASINList.Add("1886411972");
            ASINList.Add("0131495054");
            ASINList.Add("0673386023");
            ASINList.Add("1883577039");
            ASINList.Add("0262560992");
            ASINList.Add("026256100X");
            ASINList.Add("0262562146");
            //ASINList.Add("0201485419");

            IList<ProductDTO> products = productMapper.GetProducts(ASINList);

            Assert.AreEqual(ASINList.Count, products.Count);

            Debug.WriteLine(products.Count);

            foreach (IProductDTO product in products)
            {
                Assert.IsFalse((string.IsNullOrEmpty(product.ASIN)));
                Assert.IsNotNull(product.Authors);
                Assert.IsNotNull(product.Publisher);
                Debug.WriteLine(product.ASIN);
            }

            Assert.IsNotNull(products[0].Authors);

        }
    }
}