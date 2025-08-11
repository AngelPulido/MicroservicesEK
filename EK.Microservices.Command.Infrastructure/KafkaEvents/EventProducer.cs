using Confluent.Kafka;
using EK.Microservices.Command.Application.Models;
using EK.Microservices.Cqrs.Core.Events;
using EK.Microservices.Cqrs.Core.Producers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EK.Microservices.Command.Infrastructure.KafkaEvents
{
    public class EventProducer : IEventProducer
    {
        public KafkaSettings _kafkaSettings;

        public EventProducer(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
        }

        //public void Produce(string topic, BaseEvent @event)
        //{
        //    var config = new ProducerConfig
        //    {
        //        BootstrapServers = $"{_kafkaSettings.Hostname}:{_kafkaSettings.Port}"
        //    };

        //    using (var producer = new ProducerBuilder<Null, string>(config).Build())
        //    {
        //        var classEvent = @event.GetType();
        //        string value = JsonConvert.SerializeObject(@event);
        //        var message = new Confluent.Kafka.Message<Null, string> { Value = value };

        //        producer.ProduceAsync(topic, message).GetAwaiter().GetResult();
        //    }
            
        //}

        public void Produce<T>(string topic, T @event)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = $"{_kafkaSettings.Hostname}:{_kafkaSettings.Port}"
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                string value = JsonConvert.SerializeObject(@event);
                var message = new Message<Null, string> { Value = value };

                producer.ProduceAsync(topic, message).GetAwaiter().GetResult();
             }
        }
    }
}
