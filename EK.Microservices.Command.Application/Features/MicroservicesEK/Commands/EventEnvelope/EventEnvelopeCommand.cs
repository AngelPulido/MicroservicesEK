using EK.Microservices.Cqrs.Core.Events;
using MediatR;
using Newtonsoft.Json.Linq;

namespace EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EventEnvelope
{
    public class EventEnvelopeCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
        //public string EventType { get; set; } = string.Empty;
        //public string Compania { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        //public string EntityName { get; set; } = string.Empty;
        //public string User { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public object Data { get; set; }

    }
    public class EventEnvelopeWrapperDto
    {
        public EventEnvelopeCommand Item { get; set; } = new EventEnvelopeCommand();
    }
}
