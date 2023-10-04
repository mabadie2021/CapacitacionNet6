using ExampleApi.Application.Common.Interfaces;
using ExampleApi.Application.UseCase.V1.ExampleOperation.Commands;
using ExampleApi.Domain.Dtos;
using ExampleApi.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Application.Test.UseCase.V1.ExampleOperation.Commands;

public class PutExampleTest
{
      private readonly Mock<IExampleService> _exampleService;
      private readonly Mock<ILogger<PutExampleHandler>> _logger;
      private PutExampleHandler _handler;
      private CancellationToken _cancellationToken;

      public PutExampleTest()
      {
            // Arrange
            _exampleService = new Mock<IExampleService>();
            _logger = new Mock<ILogger<PutExampleHandler>>();
            _cancellationToken = CancellationToken.None;

            _handler = new PutExampleHandler(_logger.Object, _exampleService.Object);
      }

      [Fact]
      public async Task Handle_PutExample_Success()
      {
            // Arrange
            var request = new PutExample();
            request.Id = 1;
            request.RE = new()
            {
                  Name = "Test",
                  Description = "Test",
            };

            var response = new EntityExample()
            {
                  Id = 1,
                  Name = "Test",
                  Description = "Test",
            };
            // Act
            _exampleService.Setup(_ => _.UpdateExample(It.IsAny<long>(), It.IsAny<RequestExample>())).Returns(Task.Run(() => response));
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Content.Should().Be(response);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
      }

      [Fact]
      public async Task Handler_PutExample_UpdateDatabaseException()
      {
            // Arrange
            var request = new PutExample();

            _exampleService.Setup(_ => _.InsertExample(null)).ThrowsAsync(new Exception());
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Content.Should().Be(null);
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Notifications.Count().Should().Be(1);
      }
}