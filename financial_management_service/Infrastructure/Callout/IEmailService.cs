using financial_management_service.Core.Exceptions;
using financial_management_service.Core.Object;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace financial_management_service.Infrastructure.Callout
{
    public interface IMailService
    {
        Task<string> SendEmailAsync(MailRequest mailRequest);
    }

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task<string> SendEmailAsync(MailRequest mailRequest) => await Send(InitEmail(mailRequest));

        private async Task<string> Send(MimeMessage email)
        {
            var errorMessage = string.Empty;
            var smtp = new SmtpClient();
            try
            {
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, _mailSettings.EnableSSL ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                errorMessage = ex.Message;
                throw new UnhandledException(ex.Message);
            }
            finally
            {
                smtp.Disconnect(true);
            }
            return errorMessage;
        }

        private MimeMessage InitEmail(MailRequest mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail + _mailSettings.MailSubFix),
                Subject = mailRequest.Subject
            };

            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

            var builder = AddAttachments(mailRequest);

            builder.HtmlBody = mailRequest.Body;

            email.Body = builder.ToMessageBody();

            return email;
        }

        private static BodyBuilder AddAttachments(MailRequest mailRequest)
        {
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            return builder;
        }
    }

}

