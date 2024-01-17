using JwtLogin.Core.AccountContext.ValueObjects;
using JwtLogin.Core.Contexts.AccountContext.Entities;
using JwtLogin.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtLogin.Core.Contexts.AccountContext.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLogin.Core.Contexts.AccountContext.UseCases.Create
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
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

            #region 02 - Generate the objects
            Email email;
            Password password;
            User user;
            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, 400);
            }
            #endregion

            #region 03 - Check if the user exists
            try
            {
                var exists = await _repository.AnyAsync(request.Email, cancellationToken);
                if (exists)
                    return new Response("This email address is already in use", 400);
            }
            catch
            {
                return new Response("Failed to verify existing email", 500);
            }
            #endregion

            #region 04 - Persist the data
            try
            {
                await _repository.SaveAsync(user, cancellationToken);
            }
            catch
            {
                return new Response("Failed to create user", 500);
            }
            #endregion

            #region 05 - Send activation email
            try
            {
                await _service.SendVerificationEmailAsync(user, cancellationToken);
            }
            catch
            {
                // Do nothing
            }
            #endregion

            return new Response("The user has been created", new ResponseData(user.Id, user.Name, user.Email));
        }
    }
}
