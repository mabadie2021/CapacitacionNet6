using Andreani.ARQ.Pipeline.Clases;
using ExampleApi.Application.Common.Interfaces;
using ExampleApi.Domain.Dtos;
using ExampleApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleApi.Application.UseCase.V1.ExampleOperation.Commands;

public record struct PostExample : IRequest<Response<EntityExample>>
{
      public RequestExample RE { get; set; }
}

public class PostExampleHandler : IRequestHandler<PostExample, Response<EntityExample>>
{
      private readonly IExampleService _exampleService;
      private readonly ILogger<PostExampleHandler> _logger;

      public PostExampleHandler(ILogger<PostExampleHandler> logger, IExampleService exampleService)
      {
            _logger = logger;
            _exampleService = exampleService;
      }

      public async Task<Response<EntityExample>> Handle(PostExample request, CancellationToken cancellationToken)
      {
            try
            {
                  var result = await _exampleService.InsertExample(request.RE);

                  if (result == null)
                  {
                        string msj = $"No se logro insertar el Example";
                        var r = new Response<EntityExample>
                        {
                              Content = null,
                              StatusCode = System.Net.HttpStatusCode.NotFound
                        };
                        r.AddNotification(new Notify()
                        {
                              Code = "insert",
                              Property = "EntityExample",
                              Message = msj
                        });
                        _logger.LogError(msj);
                        return r;
                  }

                  return new Response<EntityExample>
                  {
                        Content = result,
                        StatusCode = System.Net.HttpStatusCode.OK
                  };
            }
            catch (Exception ex)
            {
                  string msj = $"No se logro insertar el Example, error: {ex.Message}";
                  var r = new Response<EntityExample>
                  {
                        Content = null,
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                  };
                  r.AddNotification(new Notify()
                  {
                        Code = "insert",
                        Property = "EntityExample",
                        Message = msj
                  });
                  _logger.LogError(msj);
                  return r;
            }
      }
}