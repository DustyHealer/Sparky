using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    // Example of string asserts
    [TestFixture]
    public class CustomerNUnitTests
    {
        private Customer? customer;

        [SetUp]
        public void Setup()
        {
            customer = new Customer();
        }

        [Test]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            //Arrange

            //Act
            customer.GreetAndCombineNames("Ben", "Spark");

            // Assert
            // If there are multiple asserts like this then if one fails rest of the pending asserts are not executed
            //Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark"));
            //ClassicAssert.AreEqual(customer.GreetMessage, "Hello, Ben Spark");
            //Assert.That(customer.GreetMessage, Does.Contain(",")); // If we want to check if the string contains a specific value
            //Assert.That(customer.GreetMessage, Does.Contain("Ben Spark"));
            //Assert.That(customer.GreetMessage, Does.Contain("1ben spark").IgnoreCase); // Case Insensitive
            //Assert.That(customer.GreetMessage, Does.StartWith("Hello"));
            //Assert.That(customer.GreetMessage, Does.EndWith("1Spark"));
            //Assert.That(customer.GreetMessage, Does.Match("1Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+")); // Regex matching

            // Use Multiple asserts when you want that all the asserts should run irrespective of any assert failing and then get the combined result
            Assert.Multiple(() =>
            {
                Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark"));
                ClassicAssert.AreEqual(customer.GreetMessage, "Hello, Ben Spark");
                Assert.That(customer.GreetMessage, Does.Contain(","));
                Assert.That(customer.GreetMessage, Does.Contain("Ben Spark"));
                Assert.That(customer.GreetMessage, Does.Contain("ben spark").IgnoreCase);
                Assert.That(customer.GreetMessage, Does.StartWith("Hello"));
                Assert.That(customer.GreetMessage, Does.EndWith("Spark"));
                Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
            });
        }

        [Test]
        public void GreetMessage_NotGreeted_ReturnsNull()
        {
            // Arrange

            // Act

            // Assert
            ClassicAssert.IsNull(customer.GreetMessage);
        }

        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = customer.Discount;

            // Assert a range against the result
            Assert.That(result, Is.InRange(10, 25));
        }

        [Test]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            customer.GreetAndCombineNames("Ben", "");

            ClassicAssert.IsNotNull(customer.GreetMessage);
            ClassicAssert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
            Assert.That(customer.GreetMessage, Is.Not.Null);
        }

        [Test]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
            ClassicAssert.AreEqual("Empty First Name", exceptionDetails.Message);

            Assert.That(() => customer.GreetAndCombineNames("", "Spark"), Throws.ArgumentException.With.Message.Contains("Empty First Name")); // Call the method and assert in a single line using Assert.That
        }
    }
}
