using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.Library.Common.Events.Services;
using Kalbe.Library.Message.Bus;
using MailKit.Net.Smtp;
using MimeKit;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IEmailService
    {
        bool EmailNotification(Email _data);
        Task SendEmailAsync(Email email);
    }
    public class EmailService : BaseEventService, IEmailService
    {
        private readonly IEventBus _eventBus;

        public EmailService(IEventBus eventBus) : base(eventBus)
        {
            _eventBus = eventBus;
        }
        public bool EmailNotification(Email _data)
        {
            bool isSendEmailSuccess = false;
            try
            {
                var emailSubject = _data.EmailSubject.Replace("'", "''");
                var emailBody = _data.EmailBody.Replace("'", "''");

                var emailNotification = new EmailNotificationEvent()
                {
                    SystemCode = Constant.SystemCode,
                    ModuleCode = _data.ModuleCode,
                    DocumentNumber = _data.DocumentNumber,
                    EmailTo = _data.EmailTo,
                    EmailCC = _data.EmailCC,
                    EmailBCC = _data.EmailBCC,
                    EmailSubject = emailSubject,
                    EmailBody = emailBody
                };
                _eventBus.Publish(emailNotification);
                isSendEmailSuccess = true;
                return isSendEmailSuccess;
            }
            catch (Exception ex)
            {
                return isSendEmailSuccess;
            }
        }

        //public async Task SendMail(Email mail)
        //{
        //    var email = new MimeMessage();
        //    email.To.Add(MailboxAddress("Internship Logbook Mail SYs",mail.EmailTo));
        //    email.Cc.Add(MailboxAddress.Parse(mail.EmailCC));
        //    email.Subject = mail.EmailSubject;
        //    email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //    {
        //        Text = mail.EmailBody
        //    };

        //    using var smtp = new SmtpClient();
        //    smtp.Connect();
        //    smtp.Authenticate("internshiplogbook@gmail.com", "Skrips1intern");
        //    smtp.Send(email);
        //    smtp.Disconnect();
        //}

        public async Task SendEmailAsync(Email email)
        {
            var mailSender = "internshiplogbook@outlook.com";
            var pw = "Skrips1int3rn";

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(mailSender));
            message.To.Add(MailboxAddress.Parse(email.EmailTo));
            message.Cc.Add(MailboxAddress.Parse(email.EmailCC));
            message.Subject = email.EmailSubject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = email.EmailBody,
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

            smtp.Authenticate(mailSender, pw);

            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
