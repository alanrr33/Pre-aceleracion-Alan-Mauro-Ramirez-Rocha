using Challenge1.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace Challenge1.Repositories
{
    public class MailServiceRepository : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailServiceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration["SendGripAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("alanrrr33@gmail.com", "Miau");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
