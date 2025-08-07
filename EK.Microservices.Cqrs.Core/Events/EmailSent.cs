namespace EK.Microservices.Cqrs.Core.Events
{
    public class EmailSent : BaseEvent
    {

        public string[] To { get; set; } = Array.Empty<string>(); //
        public string Subject { get; set; } = string.Empty; //
        public string Body { get; set; } = string.Empty; //
        public string NameFile { get; set; } = string.Empty; //
        public string Base64Content { get; set; } = string.Empty; //
        //public string FromEmail { get; set; } = string.Empty;
        //public string DisplayName { get; set; } = string.Empty; 
        
        
        
       

        public EmailSent(
            string id, 
            string[] to, 
            //string fromEmail, 
            //string displayName, 
            string subject, 
            string body, 
            string nameFile, 
            string base64Content) 
            : base(id)
        {
            To = to;
            //FromEmail = fromEmail;
            //DisplayName = displayName;
            Subject = subject;
            Body = body;
            NameFile = nameFile;
            Base64Content = base64Content;
        }
    }
}
