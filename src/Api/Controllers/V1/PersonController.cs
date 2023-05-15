using Andreani.ARQ.Pipeline.Clases;
using Andreani.ARQ.WebHost.Controllers;
using ExampleApi.Application.UseCase.V1.PersonOperation.Commands.Create;
using ExampleApi.Application.UseCase.V1.PersonOperation.Commands.Update;
using ExampleApi.Application.UseCase.V1.PersonOperation.Queries.GetList;
using ExampleApi.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonController : ApiControllerBase
{
    /// <summary>
    /// Creación de nueva persona
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePersonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreatePersonCommand body) => Result(await Mediator.Send(body));

    /// <summary>
    /// Listado de persona de la base de datos
    /// </summary>
    /// <remarks>en los remarks podemos documentar información más detallada</remarks>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListPerson()));

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, UpdatePersonVm body)
    {
        return Result(await Mediator.Send(new UpdatePersonCommand
        {
            PersonId = id,
            Apellido = body.Apellido,
            Nombre = body.Nombre
        }));
    }

}
