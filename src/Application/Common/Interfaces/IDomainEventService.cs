using ExampleApi.Domain.Common;
using System.Threading.Tasks;

namespace ExampleApi.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
