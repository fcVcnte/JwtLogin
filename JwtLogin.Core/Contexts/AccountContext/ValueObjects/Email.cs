using JwtLogin.Core.Contexts.AccountContext.ValueObjects;
using JwtLogin.Core.Contexts.SharedContext.Extensions;
using JwtLogin.Core.Contexts.SharedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JwtLogin.Core.AccountContext.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        //protected Email() { }
        public Email(string address)
        {
            if (string.IsNullOrEmpty(address)) 
                throw new Exception("Invalid e-mail address");

            Address = address.Trim().ToLower();
            if (Address.Length < 5)
                throw new Exception("Invalid e-mail address");
            
            if (!EmailRegex().IsMatch(address))
                throw new Exception("Invalid e-mail address");
        }

        public string Address { get; }
        public string Hash => Address.ToBase64();
        public Verification Verification { get; private set; } = new();

        public void ResendVerification()
            => Verification = new Verification();

        public static implicit operator string(Email email)
            => email.ToString();
        public static implicit operator Email(string address)
            => new(address);
        public override string ToString() 
            => Address;

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}
