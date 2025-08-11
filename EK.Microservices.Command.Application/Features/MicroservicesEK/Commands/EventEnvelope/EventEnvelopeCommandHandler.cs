using EK.Microservices.Command.Application.Aggregates;
using EK.Microservices.Cqrs.Core.Handlers;
using MediatR;

namespace EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EventEnvelope
{
    public class EventEnvelopeCommandHandler : IRequestHandler<EventEnvelopeCommand, bool>
    {
        private readonly IEventSourcingHandler<EventAggregate> _eventSourcingHandler;

        public EventEnvelopeCommandHandler(IEventSourcingHandler<EventAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(EventEnvelopeCommand request, CancellationToken cancellationToken)
        {
            var agreggate = new EventAggregate(request);
            await _eventSourcingHandler.Save(agreggate);
            return true;
        }
    }
}
