using FluentAssertions;
using ObjectClassLibrary;

namespace TestObjectClassLibrary
{
    public class BodyMassIndexUnitTest
    {
        [Fact]
        public void BMI_ValidValues_ShouldReturnBmiValue()
        {
            // Given - Arrange
            var name = "Jack Black";
            var weight = 180;
            var height = 65;

            // When - Act
            var actual = new BodyMassIndex(name, weight, height);

            // Then - Assert
            actual.Name.Should().Be(name);
            actual.Weight.Should().Be(weight);  
            actual.Height.Should().Be(height);
            actual.Bmi().Should().BeApproximately(30.0, 0);

        }

        [Fact]
        public void BMICategory_ValidValues_ShouldReturnBmiCategory()
        {
            // Given - Arrange
            var name = "Jack Black";
            var weight = 180;
            var height = 65;

            // When - Act
            var actual = new BodyMassIndex(name, weight, height);

            // Then - Assert
            actual.Name.Should().Be(name);
            actual.Weight.Should().Be(weight);
            actual.Height.Should().Be(height);
            actual.BmiCategory().Should().Be("obese");

        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void Name_BlankName_ThrowsArgumentNullException(string name)
        {
            // Given - Arrange
            var weight = 180;
            var height = 65;

            // When - Act
            var action = () => new BodyMassIndex(name, weight, height);

            // Then - Assert
            action.Should().Throw<ArgumentNullException>();
                //.WithMessage("Name cannot be blank");
            
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Height_NonPositive_ThrowsArgumentNullException(double height)
        {
            // Given - Arrange
            var weight = 180;
            var name = "Jack Black";

            // When - Act
            var action = () => new BodyMassIndex(name, weight, height);

            // Then - Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
                //.WithMessage("Height must be a positive non-zero number");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Weight_NonPositive_ThrowsArgumentNullException(double weight)
        {
            // Given - Arrange
            var height = 65;
            var name = "Jack Black";

            // When - Act
            var action = () => new BodyMassIndex(name, weight, height);

            // Then - Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
            //.WithMessage("Weight must be a positive non-zero number");
        }

        [Theory]
        [InlineData("Underweight Person1",90,60,"underweight")]
        [InlineData("Underweight Person2", 120, 75,"underweight")]
        [InlineData("Normal weight Person1", 111, 65, "normal")]
        [InlineData("Normal weight Person2", 149, 65, "normal")]
        [InlineData("Overweight Person1", 150, 65, "overweight")]
        [InlineData("Overweight Person2", 179, 65, "overweight")]
        [InlineData("Obese Person1", 180, 65, "obese")]
        [InlineData("Obese Person2", 210, 65, "obese")]
        public void BmiCategory_AllCategory_ShouldReturnCatgeory(
            string name,
            double weight,
            double height,
            string expectedCategory)
        {
            // Given - Arrange
            var currentBMI = new BodyMassIndex(name,weight,height);

            // When - Act
            var actual = currentBMI.BmiCategory();

            // Then - Assert
            actual.Should().Be(expectedCategory);
        }

    }
}