using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    // Example of string asserts
    public class CustomerXUnitTests
    {
        private readonly Customer customer;
        public CustomerXUnitTests()
        {
            customer = new Customer();

        }

        [Fact]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            //Arrange

            //Act
            customer.GreetAndCombineNames("Ben", "Spark");

            Assert.Multiple(() =>
            {
                Assert.Equal("Hello, Ben Spark", customer.GreetMessage);
                Assert.Contains(",", customer.GreetMessage);
                Assert.Contains("Ben Spark", customer.GreetMessage);
                Assert.Contains("ben spark", customer.GreetMessage.ToLower());
                Assert.StartsWith("Hello", customer.GreetMessage);
                Assert.EndsWith("Spark", customer.GreetMessage);
                Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", customer.GreetMessage);
            });
        }

        [Fact]
        public void GreetMessage_NotGreeted_ReturnsNull()
        {
            // Arrange

            // Act

            // Assert
            Assert.Null(customer.GreetMessage);
        }

        [Fact]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = customer.Discount;

            // Assert a range against the result
            Assert.InRange(result, 10, 25);
        }

        [Fact]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            customer.GreetAndCombineNames("Ben", "");

            Assert.NotNull(customer.GreetMessage);
            Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Fact]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            // When we want to check the exception message
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
            Assert.Equal("Empty First Name", exceptionDetails.Message);

            // When we only want to check that an exception is thrown
            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
        }

        [Fact]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
        {
            // Arrange
            customer.OrderTotal = 10;

            // Act
            var result = customer.GetCustomerDetails();

            // Assert
            Assert.IsType<BasicCustomer>(result); // To check the type of the object
        }

        [Fact]
        public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnPlatinumCustomer()
        {
            // Arrange
            customer.OrderTotal = 110;

            // Act
            var result = customer.GetCustomerDetails();

            // Assert
            Assert.IsType<PlatinumCustomer>(result); // To check the type of the object
        }
    }
}
