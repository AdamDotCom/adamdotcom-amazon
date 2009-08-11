using System.Diagnostics;
using AdamDotCom.Amazon.Domain.Extensions;
using AdamDotCom.Amazon.WebServiceTranslator;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class FormatHelperTest
    {      
        [Test]
        public void ShouldSortNames()
        {
            string firstName = "1FirstName";
            string secondName = "2_name";
            string thirdName = "3Third_name";
            string fourthName = "4thName";
            string namesOrdered = FormatHelper.OrderFirstLastAndMiddeNamesForMla(firstName + " " + secondName + " " + thirdName + " " + fourthName);

            Assert.IsTrue(namesOrdered.StartsWith(fourthName), namesOrdered);
            Debug.WriteLine(namesOrdered);

            namesOrdered = FormatHelper.OrderFirstLastAndMiddeNamesForMla("Martin Fowler");

            Assert.AreEqual("Fowler Martin", namesOrdered, namesOrdered);
        }

        [Test]
        public void ShouldSortAuthorsByMLA()
        {
            string[] authors = {"Martin Fowler", "Kent Beck", "John Brant", "William Opdyke", "Don Roberts"};

            var authorsInMLA = new ProductDTO {Authors = authors}.AuthorsInMlaFormat();

            Assert.IsFalse(string.IsNullOrEmpty(authorsInMLA));
            Assert.AreEqual("Fowler Martin, et al.", authorsInMLA);
            Debug.WriteLine(authorsInMLA);

            string[] edgeCaseAuthors = { "Daniel P. Friedman", "William E. Byrd", "Oleg Kiselyov" };
            authorsInMLA = new ProductDTO {Authors = edgeCaseAuthors}.AuthorsInMlaFormat();

            Assert.AreEqual("Friedman P. Daniel, Byrd E. William, and Kiselyov Oleg.", authorsInMLA);
            Debug.WriteLine(authorsInMLA);

            string[] fewAuthors = { "Daniel P. Friedman", "William E. Byrd" };
            authorsInMLA = new ProductDTO {Authors = fewAuthors}.AuthorsInMlaFormat();

            Assert.AreEqual("Friedman P. Daniel, and Byrd E. William.", authorsInMLA);
            Debug.WriteLine(authorsInMLA);
        }

        [Test]
        public void ShouldReturnAuthors()
        {
            string[] authors = { "Martin Fowler", "Kent Beck", "John Brant", "William Opdyke", "Don Roberts" };

            string authorsFlattened = new ProductDTO {Authors = authors}.Authors();

            Assert.IsFalse(string.IsNullOrEmpty(authorsFlattened));
            Assert.AreEqual("Martin Fowler, Kent Beck, John Brant, William Opdyke, Don Roberts", authorsFlattened);
            Debug.WriteLine(authorsFlattened);
        }
    }
}