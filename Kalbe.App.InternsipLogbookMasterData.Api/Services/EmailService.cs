using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IEmailServicie
    {

    }
    public class EmailService
    {

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
