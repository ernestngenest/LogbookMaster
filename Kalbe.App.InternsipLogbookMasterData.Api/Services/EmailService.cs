using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.Library.Message.Bus;
using MailKit.Net.Smtp;
using MimeKit;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IEmailServicie
    {
        bool EmailNotification(Email _data);
    }
    public class EmailService
    {
        private readonly IEventBus _eventBus;

        public EmailService(IEventBus eventBus)
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
    }
}
