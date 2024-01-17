using JwtLogin.Core.Contexts.AccountContext.Entities;
using JwtLogin.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtLogin.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Infra.Contexts.AccountContext.UseCases.Create
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking()
                .AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task SaveAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
