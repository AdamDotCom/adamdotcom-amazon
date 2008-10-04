using System.Collections.Generic;
using System.Diagnostics;
using AdamDotCom.Amazon.WebServiceTranslator;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class ListMapperTest
    {
        [TestFixtureSetUp]
        protected void SetUp()
        {
        }

        [Test]
        public void Integration_ShouldBeAbleToGetListItemsFromAmazon()
        {
            IListMapper listMapper = new ListMapper("1MRFMGASE6CQKS2WTMR2", "adamkahtavaap-20", "3JU6ASKNUS7B8");

            IList<ListItemDTO> listItems = listMapper.GetList();

            Assert.AreNotEqual(0, listItems.Count);

            Assert.Greater(listItems.Count, 10);

            foreach (IListItemDTO item in listItems)
            {
                Assert.IsFalse((string.IsNullOrEmpty(item.ASIN)));
                Debug.WriteLine(item.ASIN);
            }
        }

    }
}