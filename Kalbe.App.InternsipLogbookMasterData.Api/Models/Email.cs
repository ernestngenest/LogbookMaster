using Kalbe.Library.Message.Events;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    public class Email
    {
        public string ModuleCode { get; set; }
        public string DocumentNumber { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }

    public class EmailNotificationEvent : Event
    {
        public EmailNotificationEvent()
        {
            this.Name = "EmailQueue";
            this.TimeStamp = DateTime.Now;
        }

        public string SystemCode { get; set; }
        public string ModuleCode { get; set; }
        public string DocumentNumber { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string ProfileName { get; set; }
    }
}
