using Andreani.ARQ.Pipeline.Clases;
using ExampleApi.Application.Common.Interfaces;
using ExampleApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleApi.Application.UseCase.V1.ExampleOperation.Queries;

public record struct GetExampleById : IRequest<Response<EntityExample>>
{
      public long Id { get; set; }
}

public class GetExampleByIdHandler : IRequestHandler<GetExampleById, Response<EntityExample>>
{
      private readonly IExampleService _exampleService;
      private readonly ILogger<GetExampleByIdHandler> _logger;

      public GetExampleByIdHandler(ILogger<GetExampleByIdHandler> logger, IExampleService exampleService)
      {
            _logger = logger;
            _exampleService = exampleService;
      }

      public async Task<Response<EntityExample>> Handle(GetExampleById request, CancellationToken cancellationToken)
      {
            try
            {
                  if (request.Id <= 0)
                  {
                        string msj = $"El id pasado no es valido, el id pasado es {request.Id}";
                        var r = new Response<EntityExample>
                        {
                              Content = null,
                              StatusCode = System.Net.HttpStatusCode.NotFound
                        };
                        r.AddNotification(new Notify()
                        {
                              Code = "get",
                              Property = "EntityExample",
                              Message = msj
                        });

                        _logger.LogError(msj);

                        return r;
                  }

                  var result = await _exampleService.GetExampleById(request.Id);

                  if (result == null)
                  {
                        string msj = $"No se logro obtener el Example con el id {request.Id}";

                        var r = new Response<EntityExample>
                        {
                              Content = null,
                              StatusCode = System.Net.HttpStatusCode.NotFound
                        };
                        r.AddNotification(new Notify()
                        {
                              Code = "get",
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
                  string msj = $"Error interno de la api, error: {ex.Message}";

                  var r = new Response<EntityExample>
                  {
                        Content = null,
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                  };
                  r.AddNotification(new Notify()
                  {
                        Code = "system",
                        Property = "EntityExample",
                        Message = msj
                  });

                  _logger.LogError(msj);

                  return r;
            }
      }
}