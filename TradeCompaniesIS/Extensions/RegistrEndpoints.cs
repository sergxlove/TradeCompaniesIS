using TradeCompanyIS.Endpoints;

namespace TradeCompanyIS.Extensions
{
    public static class RegistrEndpoints
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapTradeOperationsEndpoints();
            app.MapLoginEndpoints();
            app.MapGetEndpoints();
            return app;
        }
    }
}
