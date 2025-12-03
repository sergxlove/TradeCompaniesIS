using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Application.Requests;
using TradeCompanyIS.Core.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.Requests;

namespace TradeCompanyIS.Endpoints
{
    public static class LoginEndpoints
    {
        public static IEndpointRouteBuilder MapLoginEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (HttpContext context,
                [FromBody] LoginRequest request,
                [FromServices] IUsersService userService,
                [FromServices] IJwtProviderService jwtService,
                CancellationToken token) =>
            {
                try
                {
                    if (request.Username == string.Empty || request.Password == string.Empty)
                        return Results.BadRequest("login or password is empty");
                    if (!await userService.VerifyAsync(request.Username, request.Password, token))
                        return Results.BadRequest("no auth");
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, "user"),
                    };
                    var jwttoken = jwtService.GenerateToken(new JwtRequest()
                    {
                        Claims = claims
                    });
                    context.Response.Cookies.Append("jwt", jwttoken!);
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireRateLimiting("LoginPolicy");

            app.MapPost("/reg", async (HttpContext context,
                [FromBody] RegistrRequest request,
                [FromServices] IUsersService userService,
                [FromServices] IJwtProviderService jwtService,
                [FromServices] IPasswordHasherService passwordHasher,
                CancellationToken token) =>
            {
                try
                {
                    if (request.Username == string.Empty || request.Password == string.Empty ||
                        request.AgainPassword == string.Empty)
                        return Results.BadRequest("login or password is empty");
                    if (request.Password != request.AgainPassword)
                        return Results.BadRequest("passwords is not equals");
                    var user = Users.Create(Guid.NewGuid(), request.Username, request.Password,
                        "user", passwordHasher);
                    if (!user.IsSuccess) return Results.BadRequest(user.Error);
                    var result = await userService.CreateAsync(user.Value, token);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, "user"),
                    };
                    var jwttoken = jwtService.GenerateToken(new JwtRequest()
                    {
                        Claims = claims
                    });
                    context.Response.Cookies.Append("jwt", jwttoken!);
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireRateLimiting("GeneralPolicy");

            app.MapGet("/logout", (HttpContext context) =>
            {
                context.Response.Cookies.Delete("jwt");
                return Results.Ok();
            }).RequireAuthorization("OnlyForAuthUser")
            .RequireRateLimiting("GeneralPolicy");
            return app;
        }
    }
}
