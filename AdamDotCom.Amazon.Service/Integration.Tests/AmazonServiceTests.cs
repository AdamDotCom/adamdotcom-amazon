using NUnit.Framework;
using AdamDotCom.Amazon.Service.Proxy;

namespace AdamDotCom.Amazon.Service.Integration.Tests
{
    [TestFixture]
    public class AmazonServiceTests
    {
        [Test]
        public void ShouldReturnValue()
        {
            var test = new AmazonService();

            Assert.AreEqual("Hello, adam",test.Greet("adam"));
        }
    }
}
