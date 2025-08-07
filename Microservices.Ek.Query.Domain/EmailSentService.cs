using Microservices.Ek.Query.Domain.Common;

namespace Microservices.Ek.Query.Domain
{
    public class EmailSentService : BaseDomainModel
    {
        public string[] To { get; set; } = Array.Empty<string>(); //
        public string Subject { get; set; } = string.Empty; //
        public string Body { get; set; } = string.Empty; //
        public string NameFile { get; set; } = string.Empty; //
        public string Base64Content { get; set; } = string.Empty; //
    }
}
