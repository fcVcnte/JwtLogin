using JwtLogin.Core.Contexts.AccountContext.UseCases.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JwtLogin.Api.Extensions
{
    public static class AccountContextExtension
    {
        public static void AddAccountContext(this WebApplicationBuilder builder)
        {
            #region Create
            builder.Services.AddTransient
                <Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository, 
                Infra.Contexts.AccountContext.UseCases.Create.Repository>();

            builder.Services.AddTransient
                <Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
                Infra.Contexts.AccountContext.UseCases.Create.Service>();
            #endregion
        }

        public static void MapAccountEndpoints(this WebApplication app)
        {
            #region Create
            app.MapPost("api/v1/users",
                async (Request request,
                IRequestHandler<Request, Response> handler) =>
                {
                    var result = await handler.Handle(request, new CancellationToken());
                    if (!result.IsSuccess)
                        return Results.Json(result, statusCode: result.Status);
                    return Results.Created($"api/v1/users/{result.Data?.Id}", result);
                });
            #endregion
        }
    }
}
