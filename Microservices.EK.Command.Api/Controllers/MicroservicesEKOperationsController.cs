using EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EmailSent;
using EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EventEnvelope;
using EK.Microservices.Cqrs.Core.Events;
using EK.Microservices.Cqrs.Core.Producers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Microservices.EK.Command.Api.Controllers
{
    [ApiController]
    public class MicroservicesEKOperationsController : BaseKontrollerOpen
    {
        private readonly IEventProducer _eventProducer;
        private IMediator _mediator;

        public MicroservicesEKOperationsController(IEventProducer eventProducer, IMediator mediator)
        {
            _eventProducer = eventProducer;
            _mediator = mediator;
        }

        [Route("API/KAFKA/endpoint/v1")]
        [HttpPost]
        public async Task<ActionResult> KafkaEndPoint([FromBody] EventEnvelopeWrapperDto wrapper)
        {
            if (wrapper == null || wrapper.Item == null)
                return BadRequest(new { error = "Payload inválido" });

            var dto = wrapper.Item;
            
            JObject jsonData = ConvertToJObject(dto.Data);

            var eventEnvelope = MapToEventEnvelope(dto);

            string kafkaTopic = BuildKafkaTopic(dto.Topic);


            string topicName = dto.Topic ?? string.Empty;
            string[] topicParts = topicName.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);

            dynamic _retValue = new ExpandoObject();

            try
            {
                var retValue = "";
                dynamic resultado = new ExpandoObject();

                switch (topicParts[0])
                {
                    case "notificacion":
                        ProduceEvent(kafkaTopic, eventEnvelope);
                        resultado = await HandleNotificacion(topicParts[1], jsonData);
                        retValue = JsonConvert.SerializeObject(resultado);
                        break;
                    case "correo":
                        ProduceEvent(kafkaTopic, eventEnvelope);
                        break;
                    case "actualizacion":
                        ProduceEvent(kafkaTopic, eventEnvelope);
                        break;
                    case "sql":
                        ProduceEvent(kafkaTopic, eventEnvelope);
                        break;
                    default:
                        
                        break;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok();
        }

        private async Task<ExpandoObject> HandleNotificacion(string comando, JObject json_datos)
        {
            dynamic resultado = new ExpandoObject();

            switch(comando)
            {
                case "autorizacion":
                    break;
                default:
                    break;
            }
            return resultado;
        }


        private void ProduceEvent(string kafkaTopic, object eventEnvelope)
        {
            _eventProducer.Produce(kafkaTopic, eventEnvelope);

        }

        private string BuildKafkaTopic(string topic)
        {
            return (topic ?? string.Empty).Trim('/').Replace("/", ".");
        }

        private object MapToEventEnvelope(EventEnvelopeCommand dto)
        {
            return new EventEnvelope(
                id: string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid().ToString() : dto.Id,
                topic: dto.Topic,
                timestamp: dto.Timestamp == default ? DateTime.UtcNow : dto.Timestamp,
                data: dto.Data
            );
        }

        private JObject ConvertToJObject(object data)
        {
            if (data == null)
                return new JObject();

            if (data is JsonElement jsonElement)
                return JObject.Parse(jsonElement.GetRawText());

            if (data is JObject jObject)
                return jObject;

            return JObject.FromObject(data);
        }


    }
}
