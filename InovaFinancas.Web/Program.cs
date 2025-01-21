using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using InovaFinancas.Web;
using MudBlazor.Services;
using InovaFinancas.Web.Seguranca;
using Microsoft.AspNetCore.Components.Authorization;
using InovaFinancas.Core.Handlers.Conta;
using InovaFinancas.Web.Handlers;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Web.Handlers.Transacoes;
using InovaFinancas.Web.Handlers.Categorias;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackEndURL = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
Configuration.StripePublicKey = builder.Configuration.GetValue<string>("StripePublicKey") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddTransient<IContaHandler, ContaHandler>();
builder.Services.AddTransient<ITransacaoHandler, TransacaoHandler>();
builder.Services.AddTransient<ICategoriaHandler, CategoriaHandler>();
builder.Services.AddTransient<IReportHandler, ReportHandler>();
builder.Services.AddTransient<IPedidoHandler, PedidoHandler>();
builder.Services.AddTransient<IVoucherHandler, VoucherHandler>();
builder.Services.AddTransient<IProdutoHandler, ProdutoHandler>();
builder.Services.AddTransient<IStripeHandler, StripeHandler>();

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName, opt => 
		{
			opt.BaseAddress = new Uri(Configuration.BackEndURL);
		}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddLocalization();
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

await builder.Build().RunAsync();
