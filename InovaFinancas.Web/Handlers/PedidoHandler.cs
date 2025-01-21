using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using System.Net.Http.Json;

namespace InovaFinancas.Web.Handlers
{
	public class PedidoHandler(IHttpClientFactory httpClientFactory) : IPedidoHandler
	{
		private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
		public async Task<Response<Pedido?>> CancelarAsync(CancelarPedidoRequest request)
		{
			var result = await _client.PostAsJsonAsync($"v1/pedido/{request.Id}/cancelar", request);
			return await result.Content.ReadFromJsonAsync<Response<Pedido?>>() ?? new Response<Pedido?>(null,400,"Não foi possível cancelar o Pedido");
		}

		public async Task<Response<Pedido?>> CreateAsync(CreatePedidoRequest request)
		{
			var result = await _client.PostAsJsonAsync($"v1/pedido", request);
			return await result.Content.ReadFromJsonAsync<Response<Pedido?>>() ?? new Response<Pedido?>(null, 400, "Não foi possível criar o Pedido");

		}

		public async Task<Response<Pedido?>> EstornarAsync(EstornoPedidoRequest request)
		{
			var result = await _client.PostAsJsonAsync($"v1/pedido/{request.PedidoId}/estorno", request);
			return await result.Content.ReadFromJsonAsync<Response<Pedido?>>() ?? new Response<Pedido?>(null, 400, "Não foi possível estornar o Pedido");

		}

		public async Task<PageResponse<List<Pedido>?>> GetAllAsync(GetAllPedidosRequest request)
		{
			return await _client.GetFromJsonAsync<PageResponse<List<Pedido>?>>("v1/pedido") ??
			new PageResponse<List<Pedido>?>(null, 400, "Não foi possível obter a lista de Pedidos");
		}

		public async Task<Response<Pedido?>> GetByNumeroAsync(GetPedidoByNumeroRequest request)
		{
			return await _client.GetFromJsonAsync<Response<Pedido?>>($"v1/pedido/{request.Numero}") ??
			new Response<Pedido?>(null, 400, "Não foi possível obter a lista de Pedidos");
		}

		public async Task<Response<Pedido?>> PagarAsync(PagamentoPedidoRequest request)
		{
			var result = await _client.PostAsJsonAsync($"v1/pedido/{request.Numero}/pagar", request);
			return await result.Content.ReadFromJsonAsync<Response<Pedido?>>() ?? new Response<Pedido?>(null, 400, "Não foi possível pagar o Pedido");

		}
	}
}
