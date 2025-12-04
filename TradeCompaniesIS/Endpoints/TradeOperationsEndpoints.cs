using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.Requests;

namespace TradeCompanyIS.Endpoints
{
    public static class TradeOperationsEndpoints
    {
        public static IEndpointRouteBuilder MapTradeOperationsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/getClient/{id}", async (Guid id,
                HttpContext context, 
                IClientsService clientsService, 
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("id is empty");
                    Clients? client = await clientsService.GetAsync(id, token);
                    if (client is null)
                        return Results.BadRequest("client not found");
                    return Results.Ok(client);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/getOrderClient/{id}", async (Guid id,
                HttpContext context, 
                IOrdersService ordersService, 
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("id client empty");
                    List<Orders> orders = await ordersService.GetByIdClientAsync(id, token);
                    return Results.Ok(orders);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/createOrderClient", async (HttpContext context, 
                CreateOrderRequest request, 
                IOrdersService ordersService, 
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest("request is null");
                    ResultModel<Orders> order = Orders.Create(Guid.NewGuid(),
                        request.IdClients, request.IdItem,
                        DateOnly.FromDateTime(DateTime.UtcNow), request.Quantity);
                    if (!order.IsSuccess)
                        return Results.BadRequest(order.Error);
                    Guid result = await ordersService.AddAsync(order.Value, token);
                    if (result != order.Value.Id)
                        return Results.BadRequest(order.Error);
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/updatePriceItem", () =>
            {

            });

            app.MapPost("/addItem", async (HttpContext context, 
                ItemAddRequest request, 
                IItemsService itemsService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null) return Results.BadRequest();
                    ResultModel<Items> newItem = Items.Create(Guid.NewGuid(),
                        request.Name, request.Description, request.Price, 
                        request.IdWareHouse, request.QuantityWareHouse);
                    if(!newItem.IsSuccess) return Results.BadRequest(newItem.Error);
                    Guid resultId = await itemsService.AddAsync(newItem.Value, token);
                    if(resultId != newItem.Value.Id)
                        return Results.BadRequest("error");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/warehouseGet", async (HttpContext context,
                IWareHousesService warehousesService, 
                CancellationToken token) =>
            {
                try
                {
                    List<WareHouses> wareHousesFull = await warehousesService.GetAllAsync(token);
                    List<string> wareHousesName = new List<string>();
                    foreach(WareHouses warehouse in wareHousesFull)
                    {
                        wareHousesName.Add(warehouse.Address);
                    }
                    return Results.Ok(wareHousesName);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/getItem", async (Guid id, 
                HttpContext context, 
                IItemsService itemsService, 
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("id item is empty");
                    Items? item = await itemsService.GetAsync(id, token);
                    if(item is null) 
                        return Results.BadRequest("item not found");
                    return Results.Ok(item);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/addProvider", () =>
            {

            });

            app.MapDelete("/deleteItem", async (Guid id,
                HttpContext context,
                IItemsService itemsService,
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("id item is empty");
                    int result = await itemsService.DeleteAsync(id, token);
                    if (result == 0)
                        return Results.BadRequest("item is not delete");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/getProviders", async (HttpContext context, 
                IProvidersService providersService, 
                CancellationToken token) =>
            {
                try
                {
                    List<Providers> providersFull = await providersService.GetAllAsync();
                    List<string> providersName = new List<string>();
                    foreach (Providers provider in providersFull)
                    { 
                        providersName.Add(provider.Name);
                    }
                    return Results.Ok(providersName);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/getContryId", async (HttpContext context, 
                NameRequest request, 
                ICountriesService countryService, 
                CancellationToken token) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(request.Name))
                        return Results.BadRequest("Name empty");
                    Guid countriesId = await countryService.GetIdByName(request.Name, token);
                    return Results.Ok(countriesId);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/createClient", async (HttpContext context, 
                RegClientRequest request, 
                IPasswordHasherService passwordHasher, 
                IClientsService clientsService, 
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest("Request is null");
                    ResultModel<Clients> client = Clients.Create(Guid.NewGuid(), 
                        request.NameClient, request.NumberPhone, request.Email,
                        request.IdCountry, request.AddressDelivery);
                    if (!client.IsSuccess)
                        return Results.BadRequest(client.Error);
                    var resultAdd = await clientsService.AddAsync(client.Value, token);
                    if (resultAdd != client.Value.Id) return Results.BadRequest("error");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapDelete("/deleteUser/{id}", async (Guid id, 
                HttpContext context, 
                IUsersService userService, 
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("id user is empty");
                    int result = await userService.DeleteAsync(id, token);
                    if (result == 0)
                        return Results.BadRequest("user no delete");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            return app;
        }
    }
}
