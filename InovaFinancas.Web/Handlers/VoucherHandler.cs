using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using System.Net.Http.Json;

namespace InovaFinancas.Web.Handlers
{
	public class VoucherHandler(IHttpClientFactory httpClientFactory) : IVoucherHandler
	{
		private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
		public async Task<Response<Voucher?>> GetByNumeroAsync(GetVoucherByNumeroRequest request)
		{
			return await _client.GetFromJsonAsync<Response<Voucher?>>($"v1/voucher/{request.Numero}") ?? 
				new Response<Voucher?>(null,400,"Não foi possível obter o voucher");
		}
	}
}
