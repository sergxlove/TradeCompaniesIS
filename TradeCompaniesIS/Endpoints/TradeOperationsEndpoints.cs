namespace TradeCompanyIS.Endpoints
{
    public static class TradeOperationsEndpoints
    {
        public static IEndpointRouteBuilder MapTradeOperationsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/getClient", () =>
            {

            });

            app.MapGet("/getOrderClient", () =>
            {

            });

            app.MapGet("/createOrderClient", () =>
            {

            });

            app.MapGet("/searchItem", () =>
            {

            });

            app.MapGet("/updatePriceItem", () =>
            {

            });

            app.MapGet("/addItem", () =>
            {

            });

            app.MapGet("/getItem", () =>
            {

            });

            app.MapGet("/addProvider", () =>
            {

            });

            app.MapGet("/deleteItem", () =>
            {

            });

            app.MapGet("/getProvider", () =>
            {

            });

            app.MapGet("/getContry", () =>
            {

            });

            app.MapGet("/createUser", () =>
            {

            });

            app.MapGet("/updatePassword", () =>
            {

            });

            app.MapGet("/deleteUser", () =>
            {

            });
            return app;
        }
    }
}
