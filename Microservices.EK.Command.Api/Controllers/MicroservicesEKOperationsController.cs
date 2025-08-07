using EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EmailSent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;


namespace Microservices.EK.Command.Api.Controllers
{
    [ApiController]
    [Route("kontrol/microservices/endpoint/v1")]
    public class MicroservicesEKOperationsController : ControllerBase
    {
        private IMediator _mediator;

        public MicroservicesEKOperationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost("EmailSent", Name ="EmailSent")]
        //[ProducesResponseType((int) HttpStatusCode.OK)]
        //public async Task<ActionResult> EmailSent([FromBody]EmailSentCommand command)
        //{
        //    var id = Guid.NewGuid().ToString();
        //    command.Id = id;


        //    bool enviado = await _mediator.Send(command);

        //    // Envuelve el resultado en un objeto con la propiedad 'data':
        //    return Ok(new { data = enviado });
        //}
        [HttpPost("EmailSent", Name = "EmailSent")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> EmailSent([FromBody] EmailSentWrapperDto wrapper)
        {
            // 1) Valida la estructura
            if (wrapper?.Item?.Data == null)
                return BadRequest(new { error = "Payload inválido" });

            var dto = wrapper.Item.Data;

            // 2) Mapea al comando fuerte
            var command = new EmailSentCommand
            {
                Id = Guid.NewGuid().ToString(),
                To = dto.To,
                Subject = dto.Subject,
                Body = dto.Body,
                NameFile = dto.NameFile,
                Base64Content = dto.Base64Content
            };

            // 3) Dispara tu pipeline CQRS/ES
            bool enviado = await _mediator.Send(command);

            return Ok(new { data = enviado });
        }
    }


}
