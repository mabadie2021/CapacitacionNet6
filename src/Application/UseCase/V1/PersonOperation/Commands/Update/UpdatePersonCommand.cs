using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using ExampleApi.Domain.Common;
using ExampleApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleApi.Application.UseCase.V1.PersonOperation.Commands.Update;

public class UpdatePersonCommand : IRequest<Response<string>>
{
    public string PersonId { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
}
public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, Response<string>>
{
    private readonly ITransactionalRepository _repository;
    private readonly IReadOnlyQuery _query;
    private readonly ILogger<UpdatePersonHandler> _logger;

    public UpdatePersonHandler(ITransactionalRepository repository, IReadOnlyQuery query, ILogger<UpdatePersonHandler> logger)
    {
        _repository = repository;
        _query = query;
        _logger = logger;
    }

    public async Task<Response<string>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _query.GetByIdAsync<Person>(nameof(request.PersonId), request.PersonId);
        var response = new Response<string>();
        if (person is null)
        {
            response.AddNotification("#3123", nameof(request.PersonId), string.Format(ErrorMessage.NOT_FOUND_RECORD, "Person", request.PersonId));
            response.StatusCode = System.Net.HttpStatusCode.NotFound;
            return response;
        }
        person.Nombre = request.Nombre;
        person.Apellido = request.Apellido;

        _repository.Update(person);
        await _repository.SaveChangeAsync();

        return response;
    }
}
