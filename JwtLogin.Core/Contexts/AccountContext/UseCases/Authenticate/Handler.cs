using JwtLogin.Core.Contexts.AccountContext.Entities;
using JwtLogin.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;

        public Handler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            #region 01 - Validate the request
            try
            {
                var res = Specification.Ensure(request);
                if (!res.IsValid)
                    return new Response("Invalid request", 400, res.Notifications);
            }
            catch
            {
                return new Response("It was not possible to validate the request", 500);
            }
            #endregion

            #region 02 - Get user data
            User? user;
            try
            {
                user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
                if (user is null)
                    return new Response("User not found", 404);
            }
            catch
            {
                return new Response("It was not possible to recover the user", 500);
            }
            #endregion

            #region 03 - Validate user password
                if (!user.Password.Challenge(request.Password))
                    return new Response("Password is incorrect", 400);
            #endregion

            #region 04 - Check if user is active
            try
            {
                if (!user.Email.Verification.IsActive)
                    return new Response("Inative account", 400);
            }
            catch
            {
                return new Response("It was not possible to verify user account", 500);
            }
            #endregion

            #region 05 - Return user data
            try
            {
                var data = new ResponseData()
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    Roles = user.Roles.Select(x => x.Name).ToArray(),
                };

                return new Response(string.Empty, data);
            }
            catch
            {
                return new Response("It was not possible to get user account", 500);
            }
            #endregion
        }
    }
}
