using EK.Microservices.Cqrs.Core.Domain;

namespace EK.Microservices.Cqrs.Core.Handlers
{
    public interface IEventSourcingHandler<T>
    {
        Task Save(AgreggateRoot agreggate);
        Task<T> GetById(string id);
    }
}
