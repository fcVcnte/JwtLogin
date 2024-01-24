﻿using JwtLogin.Core.Contexts.AccountContext.Entities;
using JwtLogin.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using JwtLogin.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Infra.Contexts.AccountContext.UseCases.Authenticate
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
        }
    }
}
