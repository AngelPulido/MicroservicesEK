using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EK.Microservices.Cqrs.Core.Events
{
    public class EventEnvelope : BaseEvent
    {
        //public string EventType { get; set; } = string.Empty;
        //public string Compania { get; set; } = string.Empty;
        [JsonIgnore]
        public string Topic { get; set; } = string.Empty;
        //public string EntityName { get; set; } = string.Empty;
        //public string User { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public object Data { get; set; }

        public EventEnvelope(
            string id,
            //string eventType, 
            //string compania,
            string topic,
            //string entityName, 
            //string user, 
            DateTime timestamp,
            object data)
            : base(id)
        {
            Id = id;
            //EventType = eventType;
            //Compania = compania;
            Topic = topic;
            //EntityName = entityName;
            //User = user;
            Timestamp = timestamp;
            Data = data;
        }
    }
}
