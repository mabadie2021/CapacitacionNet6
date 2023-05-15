using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using ExampleApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleApi.Application.UseCase.V1.PersonOperation.Commands.Create
{
    public class CreatePersonCommand : IRequest<Response<CreatePersonResponse>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>Lucas</example>
        public string Nombre { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <example>Olivera</example>
        public string Apellido { get; set; }
    }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Response<CreatePersonResponse>>
    {
        private readonly ITransactionalRepository _repository;
        private readonly ILogger<CreatePersonCommandHandler> _logger;

        public CreatePersonCommandHandler(ITransactionalRepository repository, ILogger<CreatePersonCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<CreatePersonResponse>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = new Person
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido
            };
            _repository.Insert(entity);
            await _repository.SaveChangeAsync();
            _logger.LogDebug("the person was add correctly");

            return new Response<CreatePersonResponse>
            {
                Content = new CreatePersonResponse
                {
                    Message = "Success",
                    PersonId = entity.PersonId
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}
