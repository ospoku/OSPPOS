using DMX.Models;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using System.Net;
using System.Net.Mail;
using static DMX.Constants.Permissions;

namespace DMX.Services
{
    
    public class EmailService(UserManager<AppUser>userManager, IConfiguration configuration)
    {
        public readonly IConfiguration config = configuration;
        public readonly UserManager<AppUser> usm = userManager;
        public void SendEmail(string receipient, string subject, string body)
        {
            var smtpClient = new SmtpClient(config["EmailSettings:Server"]) 
            { Port = Convert.ToInt32(config["EmailSettings:Port"]),
                Credentials = new NetworkCredential(config["EmailSettings:UserName"], config["EmailSettings:Password"]),
                EnableSsl=true
            }; 
            var mailMessage = new MailMessage
            {
                From = new MailAddress(config["EmailSettings:FromAddress"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true }; 
            mailMessage.To.Add(receipient); smtpClient.Send(mailMessage);
        }

            // Call your async method and wait for its completion
          
        private async Task SendEmailAsync(string userId, string subject, string message)
        {
            // Your existing async logic for sending email
            var user = await usm.FindByIdAsync("4e9812b0-ca67-46c7-9c24-7caca03d6805");
            // Proceed with sending the email to the user
            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                throw new InvalidOperationException($"User with ID '{userId}' not found or has no email address.");
            }

            // Email details
            string fromEmail = "kofipoku84@gmail.com"; // Replace with your email
            string fromPassword = "Az36400@osp"; // Replace with your email password
            string smtpServer = "smtp.gmail.com"; // Replace with your SMTP server
            int smtpPort = 587; // Typically 587 for TLS

            using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true, // Set to true if the email body contains HTML
                };

                mailMessage.To.Add(user.Email); // Add recipient email address

                try
                {
                    // Send the email asynchronously
                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine($"Email sent to {user.Email} successfully.");
                }
                catch (Exception ex)
                {
                    // Log or handle errors here
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }
           
        

        public void SendSMS(string phoneNumber, string message)
        {
            // Code to send SMS
            Console.WriteLine($"SMS sent to {phoneNumber}: {message}");
        }
    }
}

