using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using ExampleApi.Domain.Dtos;
using ExampleApi.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleApi.Application.UseCase.V1.PersonOperation.Queries.GetList {

  public record struct ListPerson : IRequest<Response<List<PersonDto>>>
  {
  }

  public class ListPersonHandler : IRequestHandler<ListPerson, Response<List<PersonDto>>>
  {
    private readonly IReadOnlyQuery _query;

    public ListPersonHandler(IReadOnlyQuery query)
    {
      _query = query;
    }

    public async Task<Response<List<PersonDto>>> Handle(ListPerson request, CancellationToken cancellationToken)
    {
      var result = await _query.GetAllAsync<PersonDto>(nameof(Person));

      return new Response<List<PersonDto>>
      {
        Content = result.ToList(),
        StatusCode = System.Net.HttpStatusCode.OK
      };
    }
  }
}
