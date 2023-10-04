using ExampleApi.Domain.Dtos;
using ExampleApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApi.Application.Common.Interfaces;

public interface IExampleService
{
      public Task<EntityExample> GetExampleById(long id);

      public Task<List<EntityExample>> GetAllExample();

      public Task<EntityExample> InsertExample(RequestExample RE);

      public Task<EntityExample> UpdateExample(long id, RequestExample RE);
}