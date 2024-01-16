using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core.Contexts.AccountContext.UseCases.Create
{
    public static class Specification
    {
        public static Contract<Notification> Ensure(Request request)
            => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Name.Length, 160, "Name", "The name must be less than 160 characters")
            .IsGreaterThan(request.Name.Length, 3, "Name", "The name must be more than 3 characters")
            .IsLowerThan(request.Password.Length, 40, "Password", "The password must be less than 40 characters")
            .IsGreaterThan(request.Password.Length, 8, "Password", "The password must be more than 8 characters")
            .IsEmail(request.Email, "Email", "E-mail address is invalid");
    }
}
