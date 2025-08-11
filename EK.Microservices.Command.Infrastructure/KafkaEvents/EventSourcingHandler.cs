using EK.Microservices.Command.Application.Aggregates;
using EK.Microservices.Cqrs.Core.Domain;
using EK.Microservices.Cqrs.Core.Handlers;
using EK.Microservices.Cqrs.Core.Infrastructure;

namespace EK.Microservices.Command.Infrastructure.KafkaEvents
{
    public class EventSourcingHandler : IEventSourcingHandler<EventAggregate>
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public Task<EventAggregate> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Save(AgreggateRoot agreggate)
        {
            await _eventStore.SaveEvents(agreggate.Id, agreggate.GetUnCommitedChanges(), agreggate.GetVersion());
            agreggate.MarkChangesAsCommited();
        }
    }
}
