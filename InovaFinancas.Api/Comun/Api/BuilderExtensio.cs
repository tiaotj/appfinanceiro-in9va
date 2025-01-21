using InovaFinancas.Api.Data;
using InovaFinancas.Api.Handlers;
using InovaFinancas.Api.Models;
using InovaFinancas.Core;
using InovaFinancas.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;
using Stripe;

namespace InovaFinancas.Api.Comun.Api
{
    public static class BuilderExtensio
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.
                    GetConnectionString("DefaultConnection") ?? string.Empty;

            Configuration.BackEndUrl = builder.Configuration.GetValue<string>("BackEndUrl") ?? string.Empty;
            Configuration.FronEndUrl= builder.Configuration.GetValue<string>("FrontEndUrl") ?? string.Empty;
            ApiConfiguration.StripeApiKey= builder.Configuration.GetValue<string>("StripeApiKey") ?? string.Empty;

            StripeConfiguration.ApiKey = ApiConfiguration.StripeApiKey;
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(x => x.FullName));
        }

        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddIdentityCookies();
            builder.Services.AddAuthorization();

        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            var connStr = builder.Configuration.
                GetConnectionString(Configuration.ConnectionString);

            builder.Services.AddDbContext<AppDbConext>(x => x.UseSqlServer(Configuration.ConnectionString));

            builder.Services.AddIdentityCore<User>()
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<AppDbConext>()
                .AddApiEndpoints();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICategoriaHandler, CategoriaHandler>();
            builder.Services.AddTransient<ITransacaoHandler, TransacaoHandler>();
            builder.Services.AddTransient<IReportHandler, ReportHandler>();
            builder.Services.AddTransient<IProdutoHandler, ProdutoHandler>();
            builder.Services.AddTransient<IVoucherHandler, VoucherHandler>();
            builder.Services.AddTransient<IPedidoHandler, PedidoHandler>();
            builder.Services.AddTransient<IStripeHandler, StripeHandler>();
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                x=>x.AddPolicy(ApiConfiguration.CorsPolicyName, 
                policy => 
                policy.WithOrigins([
					Configuration.BackEndUrl,
					Configuration.FronEndUrl,
					"https://localhost:7292",
					"https://localhost:5144",
					"https://localhost:5266",
					"https://localhost:7154",
					"http://localhost:7292",
					"http://localhost:5144",
					"http://localhost:5266",
					"http://localhost:7154",
					])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                
                ));
        }


    }
}
