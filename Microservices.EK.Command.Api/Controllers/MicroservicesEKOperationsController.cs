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
    public class MicroservicesEKOperationsController : ControllerBase
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

            var eventEnvelope = MapToEventEnvelope(dto, jsonData);


            foreach (var topic in dto.Topics ?? Array.Empty<string>())
            {
                string kafkaTopic = BuildKafkaTopic(topic);


                string[] topicParts = topic.Trim('.')
                            .Split('.', StringSplitOptions.RemoveEmptyEntries);

                dynamic _retValue = new ExpandoObject();

                try
                {
                    var retValue = "";
                    dynamic resultado = new ExpandoObject();

                    switch (topicParts[0])
                    {
                        case "notificacion":
                            resultado = await HandleNotificacion(topicParts[1], jsonData, kafkaTopic, eventEnvelope);
                            retValue = JsonConvert.SerializeObject(resultado);
                            break;
                        case "EnvioCorreos":
                            resultado = await HandleEnvioCorreos(topicParts[1], jsonData, kafkaTopic, eventEnvelope);
                            retValue = JsonConvert.SerializeObject(resultado);
                            break;
                        case "actualizacion":
                            ProduceEvent(kafkaTopic, eventEnvelope);
                            resultado = await HandleActualizacion(topicParts[1], jsonData, kafkaTopic, eventEnvelope);
                            retValue = JsonConvert.SerializeObject(resultado);
                            break;
                        case "sql":
                            ProduceEvent(kafkaTopic, eventEnvelope);
                            resultado = await HandleSql(topicParts[1], jsonData, kafkaTopic, eventEnvelope);
                            retValue = JsonConvert.SerializeObject(resultado);
                            break;
                        default:
                            dynamic _obj = new ExpandoObject();

                            _retValue.code = 500;
                            _retValue.message = "Error";
                            _retValue.data = _obj;
                            break;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
               
            }
            return Ok(new
            {
                code = 200,
                message = "Topics procesados correctamente",
                wrapper
            });
            }

        private async Task<dynamic> HandleSql(string comando, JObject json_datos, string kafkaTopic, object eventEnvelope)
        {
            throw new NotImplementedException();
        }

        private async Task<dynamic> HandleActualizacion(string comando, JObject json_datos, string kafkaTopic, object eventEnvelope)
        {
            throw new NotImplementedException();
        }

        private async Task<dynamic> HandleEnvioCorreos(string comando, JObject json_datos, string kafkaTopic, object eventEnvelope)
        {
            dynamic resultado = new ExpandoObject();

            switch (comando)
            {
                case "OrdenesDeCompra":
                    ProduceEvent(kafkaTopic, eventEnvelope);
                    break;
                default:
                    break;
            }
            return resultado;
        }

        private async Task<ExpandoObject> HandleNotificacion(string comando, JObject json_datos, string kafkaTopic, object eventEnvelope)
        {
            dynamic resultado = new ExpandoObject();

            switch(comando)
            {
                case "autorizacion":
                    ProduceEvent(kafkaTopic, eventEnvelope);
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

        private object MapToEventEnvelope(EventEnvelopeCommand dto, JObject jsonData)
        {
            return new EventEnvelope(
                id: Guid.NewGuid().ToString(),
                topics: dto.Topics,
                timestamp: dto.Timestamp == default ? DateTime.UtcNow : dto.Timestamp,
                data: jsonData
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
