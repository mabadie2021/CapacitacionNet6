using ExampleApi.Application.UseCase.V1.ExampleOperation.Commands;
using ExampleApi.Domain.Dtos;
using FluentAssertions;

namespace Application.Test.UseCase.V1.ExampleOperation.Commands;

public class ValidationExampleTest
{
      [Fact]
      public async Task Validation_WithPropertyCorrect_IsValidTrue()
      {
            // Arrange
            var request = new PostExample();
            request.RE = new RequestExample()
            {
                  Description = "Test",
                  Name = "Test"
            };

            var validator = new RequestExampleValidator();

            // Act
            var result = await validator.ValidateAsync(request.RE);

            // Assert
            result.IsValid.Should().BeTrue();
      }

      [Theory]
      [InlineData("", "test")]
      [InlineData(null, "test")]
      [InlineData("", "")]
      [InlineData(null, null)]
      public async Task Validation_WithPropertyIncorrect_IsValidFalse(string name, string description)
      {
            // Arrange
            var request = new PostExample
            {
                  RE = new()
                  {
                        Name = name,
                        Description = description
                  }
            };
            var validator = new RequestExampleValidator();
            // Act
            var result = await validator.ValidateAsync(request.RE);
            // Assert
            result.IsValid.Should().BeFalse();
      }
}