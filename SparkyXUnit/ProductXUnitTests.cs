using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class ProductXUnitTests
    {
        [Fact]
        public void GetProductPrice_PlatinumCustomer_ReturnPriceWith15PercentDiscount()
        {
            Product product = new Product { Price = 50 };

            // Dont implement the interface, just for the use of mock
            var result = product.GetPrice(new Customer { IsPlatinum = true });
            Assert.Equal(40, result);
        }

        // Example of mock abuse, since we added an interface only to use the mock
        [Fact]
        public void GetProductPriceMOQAbuse_PlatinumCustomer_ReturnPriceWith15PercentDiscount()
        {
            var customer = new Mock<ICustomer>();
            customer.Setup(u => u.IsPlatinum).Returns(true);

            Product product = new Product { Price = 50 };

            // Dont implement the interface, just for the use of mock
            var result = product.GetPrice(customer.Object);
            Assert.Equal(40, result);
        }

    }
}
