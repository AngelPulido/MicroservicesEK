using EK.Microservices.Cqrs.Core.Events;

namespace EK.Microservices.Cqrs.Core.Producers
{
    public interface IEventProducer
    {
        void Produce(string topic, BaseEvent @event);
    }
}
