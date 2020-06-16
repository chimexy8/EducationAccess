using FlashMoneyApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit.Text;

namespace EducationAccessApi.Components
{
    public class UtilityHelper
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        //private readonly IUrlHelper _urlHelper;
        //private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IActionContextAccessor _actionAccessor;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UtilityHelper(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, IActionContextAccessor actionAccessor, ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            //_urlHelper = urlHelper;
            //_urlHelperFactory = urlHelperFactory;
            _hostingEnvironment = hostingEnvironment;
            _actionAccessor = actionAccessor;
            _userManager = userManager;
            HttpContextAccessor = httpContextAccessor;
            _context = context;
            _logger = loggerFactory.CreateLogger<UtilityHelper>();
        }

        [Obsolete]
        public static async Task<bool> sendEmailAsyncTest(string email, string subject, string sentmessage)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Education Acsess", "no-reply@EducationAccess.com"));
                message.To.Add(new MailboxAddress(email));
                message.Subject = subject;

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = sentmessage
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.sendgrid.net", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    //client.Authenticate("ogisp", "E4c94830-9358-4938-b41d-c47c5ccfd294");
                    client.Authenticate("apikey", "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");

                    client.Send(message);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception e)
            {
                //e.StackTrace
            }
            return false;

         

        }
    }
}
