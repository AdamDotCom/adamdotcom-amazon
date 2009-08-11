using System.Diagnostics;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class ListMapperTest
    {
        private IListMapper listMapper;

        [TestFixtureSetUp]
        protected void SetUp()
        {
            listMapper = new ListMapper("1MRFMGASE6CQKS2WTMR2", "adamkahtavaap-20", "3JU6ASKNUS7B8");
        }

        [Test]
        public void ShouldBeAbleToGetListItemsFromAmazon()
        {
            var listItems = listMapper.GetList();

            Assert.AreNotEqual(0, listItems.Count);

            Assert.Greater(listItems.Count, 10);

            foreach (var item in listItems)
            {
                Assert.IsFalse((string.IsNullOrEmpty(item.ASIN)));
                Debug.WriteLine(item.ASIN);
            }
        }

        [Test]
        public void ShouldBeAbleToGetListItemsFromAmazonWithNoErrors()
        {
            listMapper.GetList();

            var errors = listMapper.GetErrors();

            foreach (var error in errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(0, errors.Count);
        }

    }
}