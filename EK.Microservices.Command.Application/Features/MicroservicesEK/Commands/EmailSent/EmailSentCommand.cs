using MediatR;

namespace EK.Microservices.Command.Application.Features.MicroservicesEK.Commands.EmailSent
{
    public class EmailSentCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
        public string[] To { get; set; } = Array.Empty<string>(); //
        public string Subject { get; set; } = string.Empty; //
        public string Body { get; set; } = string.Empty; //
        public string NameFile { get; set; } = string.Empty; //
        public string Base64Content { get; set; } = string.Empty; //
        //public string FromEmail { get; set; } = string.Empty;
        //public string DisplayName { get; set; } = string.Empty; 
    }

    public class EmailSentWrapperDto
    {
        public EmailSentItemDto Item { get; set; } = null!;
    }

    public class EmailSentItemDto
    {
        public EmailSentCommand Data { get; set; } = null!;
    }

}
