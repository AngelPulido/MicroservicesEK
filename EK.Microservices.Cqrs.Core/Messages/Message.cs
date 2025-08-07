

namespace EK.Microservices.Cqrs.Core.Messages
{
    public abstract class Message
    {
        public string Id { get; set; } = string.Empty;

        protected Message(string id)
        {
            Id = id;    
        }
    }
}
