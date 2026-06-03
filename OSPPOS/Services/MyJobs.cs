using System.Drawing.Printing;

namespace DMX.Services
{
    public class MyJobs
    {
        private readonly EmailService _emailService;
        public MyJobs(EmailService emailService)
        {
            _emailService = emailService;
        }
        public void SendEmailJob (string recipient,string subject, string body)
        {
        _emailService.SendEmail(recipient, subject, body);
        }
    }
}
