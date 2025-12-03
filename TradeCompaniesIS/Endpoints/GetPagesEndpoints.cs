namespace TradeCompanyIS.Endpoints
{
    public static class GetPagesEndpoints
    {
        public static IEndpointRouteBuilder MapGetEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", () =>
            {
                return Results.Redirect("wwwroot/Pages/login");
            }).RequireRateLimiting("GeneralPolicy");

            app.MapGet("/adminPage", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("wwwroot/Pages/adminPage.html");
            })
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/clientPage", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("wwwroot/Pages/clientPage.html");
            })
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/loginPage", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("wwwroot/Pages/loginPage.html");
            })
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/regClientPage", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("wwwroot/Pages/regClientPage.html");
            })
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/sallerPage", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("wwwroot/Pages/sallerPage.html");
            })
            .RequireRateLimiting("GeneralPolicy");

            return app;
        }
    }
}
