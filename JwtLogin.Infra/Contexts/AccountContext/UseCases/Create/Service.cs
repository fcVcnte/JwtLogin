using JwtLogin.Core;
using JwtLogin.Core.Contexts.AccountContext.Entities;
using JwtLogin.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Infra.Contexts.AccountContext.UseCases.Create
{
    public class Service : IService
    {
        public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            var client = new SendGridClient(Configuration.SendGrid.ApiKey);
            var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
            var subject = "Verify your account";
            var to = new EmailAddress(user.Email, user.Name);
            var content = $"Verification code: {user.Email.Verification.Code}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            await client.SendEmailAsync(msg, cancellationToken);
        }
    }
}
