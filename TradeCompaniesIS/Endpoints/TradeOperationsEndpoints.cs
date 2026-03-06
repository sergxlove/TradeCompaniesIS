using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Application.Requests;
using TradeCompanyIS.Application.Services;
using TradeCompanyIS.Core.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Response;
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

            app.MapGet("/clients/get/{email}", async (string email,
                [FromServices] IClientsService clientsService,
                CancellationToken token) =>
            {
                try
                {
                    if (email == string.Empty)
                        return Results.BadRequest("Id is empty");
                    Guid resultId = await clientsService.GetIdByEmailAsync(email, token);
                    return Results.Ok(resultId);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

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
            }).RequireAuthorization("OnlyForAuthClient")
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
                    if (!newItem.IsSuccess) return Results.BadRequest(newItem.Error);
                    Guid resultId = await itemsService.AddAsync(newItem.Value, token);
                    if (resultId != newItem.Value.Id)
                        return Results.BadRequest("Failed add item");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/warehouse", async (
                [FromServices] IWareHousesService warehousesService,
                CancellationToken token) =>
            {
                try
                {
                    List<WareHouses> wareHousesFull = await warehousesService.GetAllAsync(token);
                    List<string> wareHousesName = new List<string>();
                    foreach (WareHouses warehouse in wareHousesFull)
                    {
                        wareHousesName.Add(warehouse.Address);
                    }
                    return Results.Ok(wareHousesName);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
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
                    if (item is null)
                        return Results.BadRequest("Item not found");
                    return Results.Ok(item);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
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
            }).RequireAuthorization("OnlyForAuthClient")
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
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/providers", async (
                [FromServices] IProvidersService providersService,
                CancellationToken token) =>
            {
                try
                {
                    List<Providers> providersFull = await providersService.GetAllAsync();
                    return Results.Ok(providersFull);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
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

            app.MapPost("/client/create", async (HttpContext context,
                [FromBody] RegClientRequest request,
                [FromServices] IPasswordHasherService passwordHasher,
                [FromServices] IClientsService clientsService,
                [FromServices] IUsersService userService,
                [FromServices] IJwtProviderService jwtService,
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
                    var user = Users.Create(Guid.NewGuid(), request.Username, request.Password,
                        "user", client.Value.Id, passwordHasher);
                    if (!user.IsSuccess) return Results.BadRequest(user.Error);
                    if (await userService.CheckAsync(request.Username, token))
                    {
                        return Results.BadRequest("this login is found");
                    }
                    if(await clientsService.CheckAsync(request.Email, token))
                    {
                        return Results.BadRequest("this email is found");
                    }
                    var result = await userService.CreateAsync(user.Value, token);
                    var resultAdd = await clientsService.AddAsync(client.Value, token);
                    if (resultAdd != client.Value.Id)
                        return Results.BadRequest("Failed create client");
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, "user"),
                        new Claim(ClaimTypes.Email, request.Username),
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

            app.MapGet("/users", async (
                [FromServices] IUsersService userService,
                CancellationToken token) =>
            {
                try
                {
                    List<UsersResponse> result = await userService.GetAllUsersAsync(token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapGet("/tables", async (
                [FromServices] ITableService tableService,
                CancellationToken token) =>
            {
                try
                {
                    List<TableInfoResponse> result = new List<TableInfoResponse>();
                    result = await tableService.GetTablesAsync(token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/tables/{name}/data", async (string name,
                [FromServices] ITableService tableService,
                CancellationToken token) =>
            {
                try
                {
                    TableDataResponse result = await tableService.GetTableDataAsync(name, token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapPost("/change-password", async (HttpContext context,
                [FromBody] ChangePasswordRequest request,
                [FromServices] IUsersService userService,
                [FromServices] IJwtProviderService jwtService,
                [FromServices] IPasswordHasherService passwordHasher,
                CancellationToken token) =>
            {
                try
                {
                    if (request.Username == string.Empty || request.Password == string.Empty)
                        return Results.BadRequest("login or password is empty");
                    string roleUser = await userService.GetRoleAsync(request.Username, token);
                    var user = Users.Create(Guid.NewGuid(), request.Username, request.Password,
                        roleUser, Guid.Empty, passwordHasher);
                    if (!user.IsSuccess) return Results.BadRequest(user.Error);
                    await userService.UpdatePasswordAsync(user.Value.Username, user.Value.HashPassword, token);
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapDelete("/users/{users}", async (string users,
                [FromServices] IUsersService userService,
                CancellationToken token) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(users))
                        return Results.BadRequest("Login is empty");
                    Guid userId = await userService.GetIdClientByUsernameAsync(users, token);
                    await userService.DeleteAsync(userId, token);
                    return Results.Ok();
                }
                catch
                {
                    return Results.BadRequest();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/countries", async (
                [FromServices] ICountriesService countriesService,
                CancellationToken token) =>
            {
                try
                {
                    var countries = await countriesService.GetAllAsync(token);
                    return Results.Ok(countries);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            })
            .RequireRateLimiting("GeneralPolicy");

            app.MapGet("/items", async (
                [FromServices] IItemsService itemService,
                CancellationToken token) =>
            {
                try
                {
                    var items = await itemService.GetAllAsync(token);
                    return Results.Ok(items);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            app.MapPost("/supply/create", async (
                [FromBody] CreateSupplyRequest request,
                [FromServices] ISuppliesService suppliesService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest("Request is null");
                    ResultModel<Supplies> supply = Supplies.Create(Guid.NewGuid(), request.IdProvider,
                        request.IdWarehouse, request.IdItem, request.Quantity, request.Price);
                    if (!supply.IsSuccess)
                        return Results.BadRequest(supply.Error);
                    Guid result = await suppliesService.AddAsync(supply.Value, token);

                    if (result != supply.Value.Id)
                        return Results.BadRequest("Failed to create supply");

                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthClient")
            .RequireRateLimiting("GeneralPolicy");

            return app;
        }
    }
}
