using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class CalculatorXUnitTests
    {
        [Fact]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            Calculator calc = new();

            //Act
            int result = calc.AddNumbers(10, 20);

            //Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void IsOddChecker_InputEvenNumber_ReturnFalse()
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool isOdd = calc.IsOddNumber(10);

            // Assert
            // Assert.That(isOdd, Equal(false)); // We dont have that method in xunit
            Assert.False(isOdd);
        }

        // When we want to test for different values, we can use TestCase attribute
        [Theory]
        [InlineData(11)]
        [InlineData(13)]
        [InlineData(17)]
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool isOdd = calc.IsOddNumber(a);

            // Assert
            // Assert.That(isOdd, Is.EqualTo(true));
            Assert.True(isOdd);
        }

        [Theory]
        [InlineData(10, false)]
        [InlineData(11, true)]
        public void IsOddChecker_InputNumber_ReturnTrueIfOdd(int a, bool expectedResult)
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool result = calc.IsOddNumber(a);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5.4, 10.5)] // 15.9
        [InlineData(5.43, 10.53)] // 15.96
        [InlineData(5.49, 10.59)] // 16.08
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            //Arrange
            Calculator calc = new();

            //Act
            double result = calc.AddNumbersDouble(a, b);

            //Assert
            // Delta can be used when we are working with double
            Assert.Equal(15.9, result, .3); // Passing delta which means result can be in 15.7 to 16.1
        }

        [Fact]
        public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange()
        {
            // Arrange
            // Do this in setup method
            Calculator calc = new();
            List<int> expectedOddRange = new() { 5, 7, 9 }; //5-10

            // Act
            List<int> result = calc.GetOddRange(5, 10);

            // Assert
            Assert.Equal(expectedOddRange, result); // Used to assert collections
            Assert.Contains(7, result);
            Assert.NotEmpty(result); // To check collection is not empty
            Assert.Equal(3, result.Count); // Match the count of the collection
            Assert.DoesNotContain(6, result); // To check collection does not contains a particular method
            Assert.Equal(result.OrderBy(u=>u), result); // To check that the collection is sorted in increasing order or not
            // Assert.That(result, Is.Unique); // To check that all the values in the collection are unique or not
        }
    }
}
