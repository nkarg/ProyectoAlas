using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AlasWebApplication.Infraestructure.Helpers
{
    public interface IEmail
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task SendEmailAsync(List<string> lEmail, string subject, string htmlMessage);
    }

    public class EmailManager : IEmail
    {
        private readonly SmtpClient _client;

        public EmailManager()
        {
            // Plug in your email service here to send an email.
            //return Task.FromResult(0);

            _client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("r.cadenas.edu@gmail.com", "password")
            };

            //client.Timeout = 10000;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.From = new MailAddress("admin@futsalproject.com", "Admin FSP");
            msg.To.Add(new MailAddress(email));
            msg.Subject = subject;
            msg.Body = htmlMessage;

            return _client.SendMailAsync(msg);
        }

        public Task SendEmailAsync(List<string> lEmail, string subject, string htmlMessage)
        {
            var msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.From = new MailAddress("admin@futsalproject.com", "Admin FSP");
            foreach (var item in lEmail)
            {
                msg.To.Add(new MailAddress(item));
            }
            msg.Subject = subject;
            msg.Body = htmlMessage;

            return _client.SendMailAsync(msg);
        }
    }
}
