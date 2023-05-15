using FluentAssertions;

namespace Application.Test.UseCase.V1;

public class healthTest
{
    [Fact]
    public void HealthSuccess()
    {
        // Assert
        true.Should().Be(true);
    }

    [Fact]
    public void HealthUnSuccess()
    {
        // Assert
        false.Should().Be(false);
    }
}