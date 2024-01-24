using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public static class Specification
    {
        public static Contract<Notification> Ensure(Request request)
            => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Password.Length, 40, "Password", "The password must be less than 40 characters")
            .IsGreaterThan(request.Password.Length, 8, "Password", "The password must be more than 8 characters")
            .IsEmail(request.Email, "Email", "E-mail address is invalid");
    }
}
