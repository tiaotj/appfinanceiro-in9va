using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using System.Net.Http.Json;

namespace InovaFinancas.Web.Handlers
{
	public class ProdutoHandler(IHttpClientFactory httpClientFactory) : IProdutoHandler
	{
		private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
		public async Task<PageResponse<List<Produto>?>> GetAllAsync(GetAllProdutosRequest request)
		{
			return await _client.GetFromJsonAsync<PageResponse<List<Produto>?>>("v1/produto") ??
				new PageResponse<List<Produto>?>(null, 400, "Não foi possível obter a lista de Produtos");
		}

		public async Task<Response<Produto?>> GetBySlugAsync(GetProdutoBySlugRequest request)
		{
			return await _client.GetFromJsonAsync<Response<Produto?>>($"v1/produto/{request.Slug}") ??
				new Response<Produto?>(null, 400, "Não foi possível obter o produto");
		}
	}
}
