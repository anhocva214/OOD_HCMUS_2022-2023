using System;
namespace financial_management_service.Core.Object
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string MailSubFix { get; set; }

        public MailSettings()
        {
            Mail = string.Empty;
            Password = string.Empty;
            Host = string.Empty;
            MailSubFix = string.Empty;
        }
    }

    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }

        public MailRequest()
        {
            ToEmail = string.Empty;
            Subject = string.Empty;
            Body = string.Empty;
            Attachments = new List<IFormFile>();
        }
    }
}

