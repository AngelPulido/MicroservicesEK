using EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EmailSent;
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

        public EventAggregate(EmailSentCommand command)
        {
            var emailSent = new EmailSent(
                command.Id,
                command.To,
                //command.FromEmail,
                //command.DisplayName,
                command.Subject,
                command.Body,
                command.NameFile,
                command.Base64Content);

            RaiseEvent(emailSent);
        }
        public EventAggregate(EventEnvelopeCommand command)
        {
            var timestamp = command.Timestamp == default ? DateTime.UtcNow : command.Timestamp;
            var id = string.IsNullOrEmpty(command.Id) ? Guid.NewGuid().ToString() : command.Id;

            var envelopeEvent = new EventEnvelope(
                id,
                //command.EventType,
                //command.Compania,
                command.Topic,
                //command.EntityName,
                //command.User,
                timestamp,
                command.Data);

            RaiseEvent(envelopeEvent);
        }

        public void Apply(EmailSent @event)
        {
            Id = @event.Id;
        }
        public void Apply(EventEnvelope @event)
        {
            Id = @event.Id;
        }
    }
}
