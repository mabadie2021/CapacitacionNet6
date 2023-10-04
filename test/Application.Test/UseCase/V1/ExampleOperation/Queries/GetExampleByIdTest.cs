using ExampleApi.Application.Common.Interfaces;
using ExampleApi.Application.UseCase.V1.ExampleOperation.Queries;
using ExampleApi.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Application.Test.UseCase.V1.ExampleOperation.Queries;

public class GetExampleByIdTest
{
      private readonly Mock<IExampleService> _exampleService;
      private readonly Mock<ILogger<GetExampleByIdHandler>> _logger;
      private GetExampleByIdHandler _handler;
      private CancellationToken _cancellationToken;

      public GetExampleByIdTest()
      {
            _exampleService = new Mock<IExampleService>();
            _logger = new Mock<ILogger<GetExampleByIdHandler>>();
            _cancellationToken = CancellationToken.None;
            _handler = new GetExampleByIdHandler(_logger.Object, _exampleService.Object);
      }

      [Fact]
      public async Task Handle_GetExampleById_Success()
      {
            // Arrange
            var request = new GetExampleById()
            {
                  Id = 1,
            };
            var resonse = new EntityExample()
            {
                  Id = 1,
                  Name = "Test",
                  Description = "Test",
            };
            _exampleService.Setup(_ => _.GetExampleById(It.IsAny<long>()))
                .ReturnsAsync(resonse);
            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Content.Should().Be(resonse);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
      }

      [Fact]
      public async Task Handle_GetExampleById_ThrowException()
      {
            // Arrange
            var request = new GetExampleById() { Id = 1 };
            _exampleService.Setup(_ => _.GetExampleById(It.IsAny<long>()))
                .ThrowsAsync(new Exception());
            var result = await _handler.Handle(request, _cancellationToken);
            // Act Assert
            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
      }
}