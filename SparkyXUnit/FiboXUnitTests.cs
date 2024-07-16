using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class FiboXUnitTests
    {
        private readonly Fibo fibo;
        public FiboXUnitTests()
        {
            fibo = new Fibo();
        }

        [Fact]
        public void FiboChecker_InputRange1_ReturnsFiboSeries()
        {
            // Arrange
            List<int> expectedRange = new() { 0 };
            fibo.Range = 1;

            // Act
            List<int> result = fibo.GetFiboSeries();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotEmpty(result);
                Assert.Equal(expectedRange.OrderBy(u=>u), result);
                Assert.Equal(expectedRange, result); // Used to match collections
            });
        }

        [Fact]
        public void FiboChecker_InputRange6_ReturnsFiboSeries()
        {
            // Arrange
            List<int> expectedRange = new() { 0, 1, 1, 2, 3, 5 };
            fibo.Range = 6;

            // Act
            List<int> result = fibo.GetFiboSeries();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Contains(3, result);
                Assert.Equal(6, result.Count);
                Assert.DoesNotContain(4, result);
                Assert.Equal(expectedRange, result);
            });
        }
    }
}
