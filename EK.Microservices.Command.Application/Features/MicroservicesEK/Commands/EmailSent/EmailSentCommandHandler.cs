using EK.Microservices.Command.Application.Aggregates;
using EK.Microservices.Cqrs.Core.Handlers;
using MediatR;

namespace EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EmailSent
{
    public class EmailSentCommandHandler : IRequestHandler<EmailSentCommand, bool>
    {
        private readonly IEventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public EmailSentCommandHandler(IEventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(EmailSentCommand request, CancellationToken cancellationToken)
        {
            var agreggate = new AccountAggregate(request);
            await _eventSourcingHandler.Save(agreggate);
            return true;
        }

    }
}
