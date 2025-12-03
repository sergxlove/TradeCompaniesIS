using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Application.Services;
using TradeCompanyIS.DataAccess.Postgres;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Infrastructure;
using TradeCompanyIS.DataAccess.Postgres.Repositories;
using TradeCompanyIS.Extensions;

namespace TradeCompaniesIS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TradeCompanyDbContext>(options =>
                options.UseNpgsql("User ID=postgres;Password=123;Host=localhost;" +
                "Port=5432;Database=db;"));
            builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
            builder.Services.AddScoped<IProvidersRepository, ProvidersRepository>();
            builder.Services.AddScoped<ISuppliesRepository, SuppliesRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IWareHousesRepository, WareHousesRepository>();
            builder.Services.AddScoped<IClientsService, ClientsService>();
            builder.Services.AddScoped<ICountriesService, CountriesService>();
            builder.Services.AddScoped<IItemsService, ItemsService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IProvidersService, ProvidersService>();
            builder.Services.AddScoped<ISuppliesService, SuppliesService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IWareHousesService, WareHousesService>();
            builder.Services.AddScoped<ITransactionsWork, TransactionsWork>();
            builder.Services.AddScoped<IJwtProviderService, JwtProviderService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    IConfigurationSection? jwtSettings = builder.Configuration
                        .GetSection("JwtSettings");
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidateAudience = false,
                        ValidAudience = jwtSettings["Audience"],
                        ValidateLifetime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(jwtSettings["SecretKey"]!)),
                        ValidateIssuerSigningKey = true
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["jwt"];
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyForAdmin", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "admin");
                });
                options.AddPolicy("OnlyForAuthClient", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "client");
                });
                options.AddPolicy("OnlyForProductSpec", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "productSpec");
                });
            });

            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("GeneralPolicy", opt =>
                {
                    opt.PermitLimit = 100;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 10;
                });
                options.AddFixedWindowLimiter("LoginPolicy", opt =>
                {
                    opt.PermitLimit = 5;
                    opt.Window = TimeSpan.FromMinutes(1);
                });
                options.AddTokenBucketLimiter("UploadPolicy", opt =>
                {
                    opt.TokenLimit = 10;
                    opt.ReplenishmentPeriod = TimeSpan.FromMinutes(1);
                    opt.TokensPerPeriod = 2;
                    opt.AutoReplenishment = true;
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseRateLimiter();
            app.MapAllEndpoints();

            app.Run();
        }
    }
}
