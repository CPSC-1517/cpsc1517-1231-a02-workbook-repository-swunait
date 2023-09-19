using NhlClassLibrary; // for Circle class
using FluentAssertions; // for Should() method

namespace xUnitTestProject
{
    public class CircleUnitTest
    {
        [Fact]
        public void Area_DefaultConstructor_ShouldReturnArea()
        {
            // Given - Arrange
            var currentCircle = new Circle();

            // When - Act
            var actualArea = currentCircle.Area();

            // Then - Assert
            // The area for a radius of 1 should be 3.14
            actualArea.Should().BeApproximately(3.14, 0.005);

        }

        [Fact]
        public void Area_ParameterizedConstructor_ShouldReturnArea()
        {
            // Given - Arrange
            var currentCircle = new Circle(25);

            // When - Act
            var actualArea = currentCircle.Area();

            // Then - Assert
            // The area for a radius of 25 should be 1963.49541
            actualArea.Should().BeApproximately(1963.49541, 0.000005);

        }

        [Theory]
        [InlineData(1, 6.28)]
        [InlineData(25, 157.08)]
        [InlineData(75, 471.24)]
        public void Perimeter_ParameterizedConstructor_SHouldReturnPerimeter(
            double radius,
            double expectedPerimeter)
        {
            // Given - Arrange
            var currentCircle = new Circle(radius);

            // When - Act
            var actualPerimeter = currentCircle.Perimeter();

            // Then - Assert
            actualPerimeter.Should().BeApproximately(expectedPerimeter, 0.005);
        }


    }
}