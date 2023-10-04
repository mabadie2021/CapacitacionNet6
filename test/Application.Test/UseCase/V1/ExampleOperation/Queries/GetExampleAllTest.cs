using AutoFixture;
using ExampleApi.Application.Common.Interfaces;
using ExampleApi.Application.UseCase.V1.ExampleOperation.Queries;
using ExampleApi.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Application.Test.UseCase.V1.ExampleOperation.Queries;

public class GetExampleAllTest
{
      private readonly Mock<IExampleService> _exampleService;
      private readonly Mock<ILogger<GetExampleAllHandler>> _logger;
      private GetExampleAllHandler _handler;
      private CancellationToken _cancellationToken;

      public GetExampleAllTest()
      {
            _exampleService = new Mock<IExampleService>();
            _logger = new Mock<ILogger<GetExampleAllHandler>>();
            _cancellationToken = CancellationToken.None;
            _handler = new GetExampleAllHandler(_logger.Object, _exampleService.Object);
      }

      [Fact]
      public async Task Handle_GetExampleAll_Success()
      {
            // Arrange
            var request = new GetExampleAll();
            var resonse = new Fixture().CreateMany<EntityExample>().ToList();
            _exampleService.Setup(_ => _.GetAllExample())
                .ReturnsAsync(resonse);
            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Content.Should().Equal(resonse);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
      }

      [Fact]
      public async Task Handle_GetExampleAll_ThrowException()
      {
            // Arrange
            var request = new GetExampleAll();
            _exampleService.Setup(_ => _.GetAllExample())
                .ThrowsAsync(new Exception());
            var result = await _handler.Handle(request, _cancellationToken);
            // Act Assert
            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
      }
}