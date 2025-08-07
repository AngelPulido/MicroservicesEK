
namespace Microservices.Ek_Query_Application.Models
{
    public class EmailSettings
    {
        public string FromEmail { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Servidor { get; set; } = "";
        public string Port { get; set; }  = string.Empty;
        public string Priority { get; set; } = "Normal";
        public bool EnableSSL { get; set; } = true;
        public bool TestNotification { get; set; } = true;
        public string TestNotificationEmail { get; set; } = "";
    }
}
