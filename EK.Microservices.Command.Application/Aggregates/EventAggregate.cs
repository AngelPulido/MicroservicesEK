using EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EventEnvelope;
using EK.Microservices.Cqrs.Core.Domain;
using EK.Microservices.Cqrs.Core.Events;

namespace EK.Microservices.Command.Application.Aggregates
{
    public class EventAggregate : AgreggateRoot
    {
        public EventAggregate()
        {
        }

        public EventAggregate(EventEnvelopeCommand command)
        {
            var timestamp = command.Timestamp == default ? DateTime.UtcNow : command.Timestamp;
            var id = string.IsNullOrEmpty(command.Id) ? Guid.NewGuid().ToString() : command.Id;

            var envelopeEvent = new EventEnvelope(
                id,
                //command.EventType,
                //command.Compania,
                command.Topics,
                //command.EntityName,
                //command.User,
                timestamp,
                command.Data);

            RaiseEvent(envelopeEvent);
        }

        public void Apply(EventEnvelope @event)
        {
            Id = @event.Id;
        }
    }
}
