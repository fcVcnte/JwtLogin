using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core.Contexts.AccountContext.UseCases.Create
{
    public record Request 
    (
        string Name,
        string Email,
        string Password
    ) : IRequest<Response>;
}
