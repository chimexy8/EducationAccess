using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Data
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public EmailSender()
        {
            Options = new AuthMessageSenderOptions();
        }



        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("noreply@flashmoney.com", "Flash Money"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            //msg.AddTo(new EmailAddress(email));
            msg.AddTos(new List<EmailAddress> { new EmailAddress(email, "Okoli Chima"), new EmailAddress("sadiq.oyapero@cyhermes.com") });
            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
