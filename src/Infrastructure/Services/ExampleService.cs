using Andreani.ARQ.Core.Interface;
using ExampleApi.Application.Common.Interfaces;
using ExampleApi.Domain.Dtos;
using ExampleApi.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi.Infrastructure.Services;

public class ExampleService : IExampleService
{
      private readonly IReadOnlyQuery _query;
      private readonly ITransactionalRepository _repository;
      private readonly IDbConnection _connection;

      private readonly ILogger<ExampleService> _logger;

      public ExampleService(ILogger<ExampleService> logger, IReadOnlyQuery query, ITransactionalRepository repository, IDbConnection connection)
      {
            _logger = logger;

            _query = query;
            _repository = repository;
            _connection = connection;
      }

      public async Task<List<EntityExample>> GetAllExample()
      {
            try
            {
                  var result = await _query.GetAllAsync<EntityExample>();

                  return result.ToList();
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Error al obtener todas las entidades. Error: {ex.Message}");
                  return null;
            }
      }

      public async Task<EntityExample> GetExampleById(long id)
      {
            try
            {
                  var result = await _query.GetByIdAsync<EntityExample>(id);

                  return result;
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Error al obtener entidad con id {id}. Error: {ex.Message}");
                  return null;
            }
      }

      public async Task<EntityExample> InsertExample(RequestExample RE)
      {
            try
            {
                  EntityExample e = new()
                  {
                        Name = RE.Name,
                        Description = RE.Description,
                  };

                  _repository.Insert(e);
                  await _repository.SaveChangeAsync();

                  return e;
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Error al insertar la entidad. Error: {ex.Message}");
                  return null;
            }
      }

      public async Task<EntityExample> UpdateExample(long id, RequestExample RE)
      {
            try
            {
                  EntityExample e = await GetExampleById(id);
                  if (e != null)
                  {
                        e.Name = RE.Name;
                        e.Description = RE.Description;

                        _repository.Update(e);
                        await _repository.SaveChangeAsync();

                        return e;
                  }
                  else
                  {
                        throw new Exception($"No se encontro la entidad con id {id}");
                  }
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Error al actualizar la entidad con id {id}. Error: {ex.Message}");
                  return null;
            }
      }
}