using Andreani.ARQ.Pipeline.Clases;
using Andreani.ARQ.WebHost.Controllers;
using ExampleApi.Application.UseCase.V1.ExampleOperation.Commands;
using ExampleApi.Application.UseCase.V1.ExampleOperation.Queries;
using ExampleApi.Domain.Dtos;
using ExampleApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ExampleController : ApiControllerBase
{
      /// <summary>
      /// Listado de example de la base de datos
      /// </summary>
      /// <remarks>en los remarks podemos documentar información más detallada</remarks>
      /// <returns></returns>
      [HttpGet]
      [ProducesResponseType(typeof(List<EntityExample>), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> GetAll() => Result(await Mediator.Send(new GetExampleAll()));

      /// <summary>
      /// Obtiene example de la base de datos
      /// </summary>
      /// <remarks>en los remarks podemos documentar información más detallada</remarks>
      /// <returns></returns>
      [HttpGet("{id}")]
      [ProducesResponseType(typeof(List<EntityExample>), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
      public async Task<IActionResult> GetById(long id)
      {
            return Result(await Mediator.Send(new GetExampleById
            {
                  Id = id,
            }));
      }

      /// <summary>
      /// Inserta un example de la base de datos
      /// </summary>
      /// <remarks>en los remarks podemos documentar información más detallada</remarks>
      /// <returns></returns>
      [HttpPost]
      [ProducesResponseType(typeof(List<EntityExample>), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Insert([FromBody] RequestExample RE)
      {
            return Result(await Mediator.Send(new PostExample()
            {
                  RE = RE
            }));
      }

      /// <summary>
      /// Actualiza un example de un id de la base de datos
      /// </summary>
      /// <remarks>en los remarks podemos documentar información más detallada</remarks>
      /// <returns></returns>
      [HttpPut("{id}")]
      [ProducesResponseType(typeof(List<EntityExample>), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Update(long id, [FromBody] RequestExample RE)
      {
            return Result(await Mediator.Send(new PutExample()
            {
                  Id = id,
                  RE = RE
            }));
      }
}