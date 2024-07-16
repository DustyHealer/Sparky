using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class GradingCalculatorXUnitTests
    {
        private readonly GradingCalculator gradingCalculator;

        public GradingCalculatorXUnitTests()
        {
            gradingCalculator = new GradingCalculator();

        }

        [Fact]
        public void GradeCalc_InputScore95Attendance90_GetAGrade()
        {
            // Arrange
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string result = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("A", result);
        }

        [Fact]
        public void GradeCalc_InputScore85Attendance90_GetBGrade()
        {
            // Arrange
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string result = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("B", result);
        }

        [Fact]
        public void GradeCalc_InputScore65Attendance90_GetCGrade()
        {
            // Arrange
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string result = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("C", result);
        }

        [Fact]
        public void GradeCalc_InputScore95Attendance65_GetBGrade()
        {
            // Arrange
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            // Act
            string result = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("B", result);
        }

        [Theory]
        [InlineData(95, 55)]
        [InlineData(65, 55)]
        [InlineData(50, 90)]
        public void GradeCalc_FailureScenarios_GetFGrade(int score, int attendance)
        {
            // Arrange
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            // Act
            string result = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("F", result);
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void GradeCalc_AllGradeLogicalScenarios_GradeOutput(int score, int attendance, string expectedResult)
        {
            // Arrange
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            // Act
            var result = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
