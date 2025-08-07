using Microservices.Ek.Query.Domain;

namespace Microservices.Ek_Query_Application.Contracts.Persistence
{
    public interface IAsyncServiceEmail 
    {
        Task<bool> SendEmailWithFileAsync(
            string[] to,
            string subject,
            string body,
            string nameFile,
            string base64Content);

    }
}
