using JwtLogin.Core.Contexts.AccountContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core.Contexts.AccountContext.UseCases.Create.Contracts
{
    public interface IRepository
    {
        Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
        Task SaveAsync(User user, CancellationToken cancellationToken);
    }
}
