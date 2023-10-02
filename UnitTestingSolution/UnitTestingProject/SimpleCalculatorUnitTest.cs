using FluentAssertions; // for Should() extension method
using UnitTestingClassLibrary;  // for SimpleCalculator class

namespace UnitTestingProject
{
    public class SimpleCalculatorUnitTest
    {
        /*
         *  Test method should have the following naming convention:
         *  1) Method/Property
         *  2) TestCase
         *  3) ExpectedBehavior
         *  
         *  Method_TestCase_ExpectedBehaviour
         */
        [Theory]
        [InlineData(5,8)]
        [InlineData(99,88)]
        [InlineData(250,50)]
        public void Constructor_SetPropertyValues_PropertiesSet(
            int expectedOperand1,
            int expectedOperand2)
        {
            // Inside a test method you will use the Arrange, Act, Assert pattern
            // Arrange
            //int expectedOperand1 = 5;
            //int expectedOperand2 = 8;
            var calculator = new SimpleCalculator(expectedOperand1, expectedOperand2);
            // Act
            var actualOperand1 = calculator.Operand1;
            var actualOperand2 = calculator.Operand2;
            // Assert
            actualOperand1.Should().Be(expectedOperand1);
            actualOperand2.Should().Be(expectedOperand2);

        }

        [Theory]
        [InlineData(5,8,13)]
        [InlineData(15,25,40)]
        [InlineData(250,50,300)]
        public void Add_AddsNumbers_ReturnsSum(
            int num1,
            int num2,
            int expectedSum)
        {
            // Arrange
            var calculator = new SimpleCalculator(num1, num2);
            // Act
            var actualSum = calculator.Add();
            // Asssert
            actualSum.Should().Be(expectedSum);
        }

        [Theory]
        [InlineData(5, 8, -3)]
        [InlineData(15, 25, -10)]
        [InlineData(250, 50, 200)]
        public void Subtract_SubtractNumbers_ReturnsDifference(
            int num1,
            int num2,
            int expectedDifference)
        {
            // Arrange
            var calculator = new SimpleCalculator(num1, num2);
            // Act
            var actualDifference = calculator.Subtract();
            // Asssert
            actualDifference.Should().Be(expectedDifference);
        }

        [Theory]
        [InlineData(5, 8, 40)]
        [InlineData(15, 10, 150)]
        [InlineData(250, 4, 1000)]
        public void Multiply_MultipliesNumbers_ReturnsProduct(
            int num1,
            int num2,
            int expectedProduct)
        {
            // Arrange
            var calculator = new SimpleCalculator(num1, num2);
            // Act
            var actualProduct = calculator.Multiply();
            // Asssert
            actualProduct.Should().Be(expectedProduct);
        }

        [Theory]
        [InlineData(50, 5, 10)]
        [InlineData(4, 5, 0.8)]
        [InlineData(1, 3, 0.33333)]
        public void Divide_DividesNumbers_ReturnsQuotient(
            int num1,
            int num2,
            decimal expectedQuotient)
        {
            // Arrange
            var calculator = new SimpleCalculator(num1, num2);
            // Act
            var actualQuotient = calculator.Divide();
            // Asssert
            actualQuotient.Should().BeApproximately(expectedQuotient,0.000005m);

        }

        [Fact]
        public void Divide_DivisionByZero_ThrowsException()
        {
            // Arrange
            var calculator = new SimpleCalculator(9, 0);
            // Act
            Action act = () => calculator.Divide();
            // Assert
            act.Should().Throw<DivideByZeroException>();
        }

    }
}