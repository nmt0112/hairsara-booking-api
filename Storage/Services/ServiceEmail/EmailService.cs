using Microsoft.EntityFrameworkCore;
using MimeKit;
using Storage.Models;

namespace Storage.Services.ServiceEmail
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ApplicationDbContext _context;

        public EmailService(EmailConfiguration emailConfig, ApplicationDbContext context)
        {
            _emailConfig = emailConfig;
            _context = context;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Content
            };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
        public string GetCustomerEmailById(string customerId)
        {
            var customer = _context.Customer.Include(c => c.AspNetUsers).FirstOrDefault(c => c.IdUserCustomer == customerId);
            return customer?.AspNetUsers?.Email;
        }
    }
}
