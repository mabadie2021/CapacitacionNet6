using Andreani.ARQ.Pipeline.Clases;
using Andreani.SolucionesTYD.Tools.Common;
using ExampleApi.Application.Common.Interfaces;
using ExampleApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleApi.Application.UseCase.V1.ExampleOperation.Queries;

public record struct GetExampleAll : IRequest<Response<List<EntityExample>>>
{
}

public class GetExampleAllHandler : IRequestHandler<GetExampleAll, Response<List<EntityExample>>>
{
      private readonly IExampleService _exampleService;
      private readonly ILogger<GetExampleAllHandler> _logger;

      public GetExampleAllHandler(ILogger<GetExampleAllHandler> logger, IExampleService exampleService)
      {
            _logger = logger;
            _exampleService = exampleService;
      }

      public async Task<Response<List<EntityExample>>> Handle(GetExampleAll request, CancellationToken cancellationToken)
      {
            try
            {
                  var result = await _exampleService.GetAllExample();

                  if (result == null)
                  {
                        string msj = $"No se logro obtener todos los Examples";
                        var r = new Response<List<EntityExample>>
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

                  return new Response<List<EntityExample>>
                  {
                        Content = result,
                        StatusCode = System.Net.HttpStatusCode.OK
                  };
            }
            catch (Exception ex)
            {
                  string msj = $"Error interno de la api, error: {ex.Message}";
                  _logger.LogError(msj);

                  return ResponseCreator.ErrorResponse<List<EntityExample>>(
                      System.Net.HttpStatusCode.InternalServerError,
                      "system",
                      "EntityExample",
                      msj);

                  /*var r = new Response<List<EntityExample>>
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

                  return r;*/
            }
      }
}