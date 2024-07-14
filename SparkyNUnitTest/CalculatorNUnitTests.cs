using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            Calculator calc = new();

            //Act
            int result = calc.AddNumbers(10, 20);

            //Assert
            ClassicAssert.AreEqual(30, result);
        }

        [Test]
        public void IsOddChecker_InputEvenNumber_ReturnFalse()
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool isOdd = calc.IsOddNumber(10);

            // Assert
            ClassicAssert.That(isOdd, Is.EqualTo(false));
            ClassicAssert.IsFalse(isOdd);
        }

        // When we want to test for different values, we can use TestCase attribute
        [Test]
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(17)]
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool isOdd = calc.IsOddNumber(a);

            // Assert
            ClassicAssert.That(isOdd, Is.EqualTo(true));
            ClassicAssert.IsTrue(isOdd);
        }

        [Test]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(11, ExpectedResult = true)]
        public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a)
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool result = calc.IsOddNumber(a);

            // Assert
            return result; // It will get matched to the expected result
        }

        [Test]
        [TestCase(5.4, 10.5)] // 15.9
        [TestCase(5.43, 10.53)] // 15.96
        [TestCase(5.49, 10.59)] // 16.08
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            //Arrange
            Calculator calc = new();

            //Act
            double result = calc.AddNumbersDouble(a, b);

            //Assert
            // Delta can be used when we are working with double
            ClassicAssert.AreEqual(15.9, result, .2); // Passing delta which means result can be in 15.7 to 16.1
        }

        [Test]
        public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange() 
        {
            // Arrange
            // Do this in setup method
            Calculator calc = new();
            List<int> expectedOddRange = new() { 5, 7, 9 }; //5-10

            // Act
            List<int> result = calc.GetOddRange(5, 10);
            
            // Assert
            Assert.That(result, Is.EquivalentTo(expectedOddRange)); // Used to assert collections
            //ClassicAssert.AreEqual(expectedOddRange, result);
            //ClassicAssert.Contains(7, result);
            Assert.That(result, Does.Contain(7)); // To check contains with That
            Assert.That(result, Is.Not.Empty); // To check collection is not empty
            Assert.That(result.Count, Is.EqualTo(3)); // Match the count of the collection
            Assert.That(result, Has.No.Member(6)); // To check collection does not contains a particular method
            Assert.That(result, Is.Ordered.Ascending); // To check that the collection is sorted in increasing order or not
            Assert.That(result, Is.Unique); // To check that all the values in the collection are unique or not
        }
    }
}
