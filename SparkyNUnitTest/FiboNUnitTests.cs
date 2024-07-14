using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class FiboNUnitTests
    {
        private Fibo fibo;

        [SetUp]
        public void Setup()
        {
            fibo = new Fibo();
        }

        [Test]
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
                Assert.That(result, Is.Not.Empty);
                Assert.That(result, Is.Ordered);
                Assert.That(result, Is.EquivalentTo(expectedRange)); // Used to match collections
            });
        }

        [Test]
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
                Assert.That(result, Does.Contain(3));
                Assert.That(result, Has.Count.EqualTo(6));
                Assert.That(result, Has.No.Member(4));
                Assert.That(result, Is.EquivalentTo(expectedRange));
            });
        }
    }
}
