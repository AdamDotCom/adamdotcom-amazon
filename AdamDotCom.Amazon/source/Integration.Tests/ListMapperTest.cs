﻿using System.Diagnostics;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class ListMapperTest
    {
        [Test]
        public void ShouldBeAbleToGetListItemsFromAmazon()
        {
            var listMapper = new ListMapper(TestHelper.AwsAccessKey, TestHelper.AssociateTag, TestHelper.SecretAccessKey, "3JU6ASKNUS7B8");

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
        public void ShouldBeAbleToGetListItemsFromAmazonNoPaging()
        {
            var listMapper = new ListMapper(TestHelper.AwsAccessKey, TestHelper.AssociateTag, TestHelper.SecretAccessKey, "1RVDGPM8SWXG4");

            var listItems = listMapper.GetList();

            Assert.AreNotEqual(0, listItems.Count);

            Assert.Greater(listItems.Count, 4);

            foreach (var item in listItems)
            {
                Assert.IsFalse((string.IsNullOrEmpty(item.ASIN)));
                Debug.WriteLine(item.ASIN);
            }
        }

        [Test]
        public void ShouldBeAbleToGetListItemsFromAmazonWithNoErrors()
        {
            var listMapper = new ListMapper(TestHelper.AwsAccessKey, TestHelper.AssociateTag, TestHelper.SecretAccessKey, "3JU6ASKNUS7B8");

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