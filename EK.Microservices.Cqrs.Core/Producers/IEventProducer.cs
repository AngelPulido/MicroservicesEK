using EK.Microservices.Cqrs.Core.Events;

namespace EK.Microservices.Cqrs.Core.Producers
{
    public interface IEventProducer
    {
        void Produce<T>(string topic, T @event);
    }
}
