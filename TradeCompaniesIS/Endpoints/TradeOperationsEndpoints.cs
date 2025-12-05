using Microsoft.AspNetCore.Mvc;
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
            app.MapGet("/clients/{id}", async (Guid id,
                [FromServices] IClientsService clientsService,
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("Id is empty");
                    Clients? client = await clientsService.GetAsync(id, token);
                    if (client is null)
                        return Results.BadRequest("Client not found");
                    return Results.Ok(client);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/clients/orders/{id}", async (Guid id,
                [FromServices] IOrdersService ordersService, 
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("Id client empty");
                    List<Orders> orders = await ordersService.GetByIdClientAsync(id, token);
                    return Results.Ok(orders);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapPost("/client/order/create", async (
                [FromBody] CreateOrderRequest request, 
                [FromServices] IOrdersService ordersService, 
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest("Request is null");
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
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapPost("/item/price/update", async ( 
                [FromBody] UpdatePriceItemRequest request, 
                [FromServices] IItemsService itemsService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest("Request is null");
                    int result = await itemsService.UpdatePriceAsync(request.ID,
                        request.NewPrice, token);
                    if (result == 0)
                        return Results.BadRequest("No updates price");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForProductSpec")
            .RequireRateLimiting("GeneralPolicy");

            app.MapPost("/item/add", async (
                [FromBody] ItemAddRequest request, 
                [FromServices] IItemsService itemsService,
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
                        return Results.BadRequest("Failed add item");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForProductSpec")
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/warehouse", async (
                [FromServices] IWareHousesService warehousesService, 
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
            }).RequireAuthorization("OnlyForProductSpec")
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/item", async (Guid id, 
                [FromServices] IItemsService itemsService, 
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("Id item is empty");
                    Items? item = await itemsService.GetAsync(id, token);
                    if(item is null) 
                        return Results.BadRequest("Item not found");
                    return Results.Ok(item);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForProductSpec")
            .RequireRateLimiting("GeneralPolicy");

            app.MapPost("/provider/add", async ( 
                [FromBody] AddProviderRequest request, 
                [FromServices] IProvidersService providersService, 
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest("Request is null");
                    ResultModel<Providers> provider = Providers.Create(Guid.NewGuid(),
                        request.Name, request.NumberPhone, request.IdCountry,
                        request.Address);
                    if (!provider.IsSuccess)
                        return Results.BadRequest("Failed add provider");
                    Guid result = await providersService.AddAsync(provider.Value, token);
                    if (result != provider.Value.Id)
                        return Results.BadRequest("Failed add provider");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForProductSpec")
            .RequireRateLimiting("GeneralPolicy");

            app.MapDelete("/item/delete", async (Guid id,
                [FromServices] IItemsService itemsService,
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("Id item is empty");
                    int result = await itemsService.DeleteAsync(id, token);
                    if (result == 0)
                        return Results.BadRequest("Item is not delete");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForProductSpec")
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/providers", async (
                [FromServices] IProvidersService providersService, 
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
            }).RequireAuthorization("OnlyForProductSpec")
            .RequireRateLimiting("GeneralPolicy");

            app.MapPost("/country/id", async (
                [FromBody] NameRequest request, 
                [FromServices] ICountriesService countryService, 
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
            }).RequireRateLimiting("GeneralPolicy");

            app.MapPost("/client/create", async (
                [FromBody] RegClientRequest request, 
                [FromServices] IPasswordHasherService passwordHasher, 
                [FromServices] IClientsService clientsService, 
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
                    if (resultAdd != client.Value.Id)
                        return Results.BadRequest("Failed create client");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireRateLimiting("GeneralPolicy");

            app.MapDelete("/user/delete/{id}", async (Guid id, 
                [FromServices] IUsersService userService, 
                CancellationToken token) =>
            {
                try
                {
                    if (id == Guid.Empty)
                        return Results.BadRequest("Id user is empty");
                    int result = await userService.DeleteAsync(id, token);
                    if (result == 0)
                        return Results.BadRequest("User no delete");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAdmin")
            .RequireRateLimiting("GeneralPolicy");

            return app;
        }
    }
}
