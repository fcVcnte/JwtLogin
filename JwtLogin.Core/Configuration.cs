using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core
{
    public static class Configuration
    {
        public static SecretsConfiguration Secrets { get; set; } = new();
        public static EmailConfiguration Email { get; set; } = new();
        public static SendGridConfiguration SendGrid { get; set; } = new();
        public static DatabaseConfiguration Database { get; set; } = new();

        public class DatabaseConfiguration
        {
            public string ConnectionString { get; set; } = string.Empty;
        }

        public class EmailConfiguration
        {
            public string DefaultFromEmail { get; set; } = "jwt_login@test.com";
            public string DefaultFromName { get; set; } = "JWT Login Test";
        }

        public class SendGridConfiguration
        {
            public string ApiKey { get; set; }
        }

        public class SecretsConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;
            public string JwtPrivateKey { get; set; } = string.Empty;
            public string PasswordSaltKey { get; set; } = string.Empty;
        }
    }
}
