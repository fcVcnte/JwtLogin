﻿using JwtLogin.Core.AccountContext.ValueObjects;
using JwtLogin.Core.Contexts.AccountContext.ValueObjects;
using JwtLogin.Core.Contexts.SharedContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core.Contexts.AccountContext.Entities
{
    public class User : Entity
    {
        protected User() { }

        public User(string name, string email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
        public User(string email, string? password = null)
        {
            Email = email;
            Password = new Password(password);
        }

        public string Name { get; set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public string Image { get; private set; } = string.Empty;

        public void UpdatePassword(string plainTextPassword, string code)
        {
            if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Restauration code is invalid");

            var password = new Password(plainTextPassword);
            Password = password;
        }

        public void UpdateEmail(Email email)
        {
            Email = email;
        }

        public void ChangePassword(string plainTextPassword)
        {
            var password = new Password(plainTextPassword);
            Password = password;
        }
    }
}
