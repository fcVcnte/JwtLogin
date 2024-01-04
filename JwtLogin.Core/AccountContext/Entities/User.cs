using JwtLogin.Core.AccountContext.ValueObjects;
using JwtLogin.Core.SharedContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core.AccountContext.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public Email Email { get; private set; }
        //public Password Password { get; }
    }
}
