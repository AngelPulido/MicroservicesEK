using EK.Microservices.Cqrs.Core.Messages;

namespace EK.Microservices.Cqrs.Core.Events
{
    public class BaseEvent : Message
    {
        public int Version { get; set; }
        public BaseEvent(string id) : base(id)
        {
        }
    }
}
