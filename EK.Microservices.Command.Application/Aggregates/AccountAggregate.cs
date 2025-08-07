using EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EmailSent;
using EK.Microservices.Cqrs.Core.Domain;
using EK.Microservices.Cqrs.Core.Events;

namespace EK.Microservices.Command.Application.Aggregates
{
    public class AccountAggregate : AgreggateRoot
    {
        public AccountAggregate()
        {
        }

        public AccountAggregate(EmailSentCommand command)
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

        public void Apply(EmailSent @event)
        {
            Id = @event.Id;
        }
    }
}
