using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Requests.Stripe;
using InovaFinancas.Core.Responses;
using InovaFinancas.Core.Responses.Stripe;
using System.Net.Http.Json;

namespace InovaFinancas.Web.Handlers
{
	public class StripeHandler(IHttpClientFactory httpClientFactory) : IStripeHandler
	{
		private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
		public async Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request)
		{
			var result = await _client.PostAsJsonAsync("v1/pagamento/stripe/session", request);
			return await result.Content.ReadFromJsonAsync<Response<string?>>() ?? new Response<string?>(null,400,"falha ao criar seção no stripe");
		}

		public  async Task<Response<List<StripeTransactionResponse>>> GetTransactonByPedidoNumeroAsync(GetTransactionByPedidoNumeroRequest request)
		{
			var result = await _client.PostAsJsonAsync($"v1/pagamento/stripe/{request.Numero}", request);
			return await result.Content.ReadFromJsonAsync<Response<List<StripeTransactionResponse>>>() ?? 
				new Response<List<StripeTransactionResponse>>(null, 400, "falha ao recuperar transacoes do pedido no stripe");
		}
	}
}
