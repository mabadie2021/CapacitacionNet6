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

public record struct PutExample : IRequest<Response<EntityExample>>
{
      public long Id { get; set; }
      public RequestExample RE { get; set; }
}

public class PutExampleHandler : IRequestHandler<PutExample, Response<EntityExample>>
{
      private readonly IExampleService _exampleService;
      private readonly ILogger<PutExampleHandler> _logger;

      public PutExampleHandler(ILogger<PutExampleHandler> logger, IExampleService exampleService)
      {
            _logger = logger;
            _exampleService = exampleService;
      }

      public async Task<Response<EntityExample>> Handle(PutExample request, CancellationToken cancellationToken)
      {
            try
            {
                  var result = await _exampleService.UpdateExample(request.Id, request.RE);

                  if (result == null)
                  {
                        string msj = $"No se logro actualizar el Example con el id {request.Id}";
                        var r = new Response<EntityExample>
                        {
                              Content = null,
                              StatusCode = System.Net.HttpStatusCode.NotFound
                        };
                        r.AddNotification(new Notify()
                        {
                              Code = "update",
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
                  string msj = $"No se logro actualizar el Example con el id {request.Id}, Error: {ex.Message}";
                  var r = new Response<EntityExample>
                  {
                        Content = null,
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                  };
                  r.AddNotification(new Notify()
                  {
                        Code = "update",
                        Property = "EntityExample",
                        Message = msj
                  });
                  _logger.LogError(msj);
                  return r;
            }
      }
}