using EK.Microservices.Cqrs.Core.Events;
using EK.Microservices.Cqrs.Core.Infrastructure;
using EK.Microservices.Cqrs.Core.Producers;

namespace EK.Microservices.Command.Infrastructure.KafkaEvents
{
    public class EventStore : IEventStore
    {
        private readonly IEventProducer _eventProducer;

        public EventStore(IEventProducer eventProducer)
        {
            _eventProducer = eventProducer;
        }

        public Task<List<BaseEvent>> GetEvents(string aggregateId)
        {
            throw new NotImplementedException();
        }

        public Task SaveEvents(string aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            if (expectedVersion != -1)
            {
                throw new Exception("Errores de concurrencia");
            }
            var version = expectedVersion;
            foreach (var evt in events)
            {
                version++;
                evt.Version = version;

                _eventProducer.Produce(evt.GetType().Name, evt);
            }

            return Task.CompletedTask;
        }
    }
}
