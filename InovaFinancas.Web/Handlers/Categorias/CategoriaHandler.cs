using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Responses;
using System.Net.Http.Json;

namespace InovaFinancas.Web.Handlers.Categorias
{
	public class CategoriaHandler(IHttpClientFactory httpClientFactory) : ICategoriaHandler
	{
		private readonly HttpClient _client= httpClientFactory.CreateClient(Configuration.HttpClientName);
		public async Task<Response<Categoria?>> CreateAsync(CreateCategoriaRequest request)
		{
			var result =  await _client.PostAsJsonAsync("v1/categoria", request);
			return  await result.Content.ReadFromJsonAsync<Response<Categoria?>>() 
				?? new Response<Categoria?>(null,400,"Falha ao criar a categoria");
		}

		public async Task<Response<Categoria?>> DeleteAsync(DeleteCategoriaRequest request)
		{
			var result = await _client.DeleteAsync($"v1/categoria/{request.Id}");
			return await result.Content.ReadFromJsonAsync<Response<Categoria?>>()
				?? new Response<Categoria?>(null, 400, "Falha ao excluir a categoria");
		}

		public async Task<PageResponse<List<Categoria>>> GetAllAsync(GetAllCategoriaRequest request)
		{
			return await _client.GetFromJsonAsync<PageResponse<List<Categoria>>>("v1/categoria?tamanhoPagina=25&numeroPagina=1") ??
				new PageResponse<List<Categoria>>(null,400,"Falha ao buscar todas as categorias");
		}

		public async Task<Response<Categoria?>> GetByIdAsync(GetCategoriaByIdRequest request)
		{
			var result = await _client.GetFromJsonAsync<Response<Categoria?>> ($"v1/categoria/{request.Id}");
			return result ?? new Response<Categoria?>(null, 400, "Falha ao buscar a categoria");
		}

		public  async Task<Response<Categoria?>> UpdateAsync(UpdateCategoriaRequest request)
		{
			var result = await _client.PutAsJsonAsync($"v1/categoria/{request.Id}", request);
			return await result.Content.ReadFromJsonAsync<Response<Categoria?>>()
				?? new Response<Categoria?>(null, 400, "Falha ao atualizar a categoria");
		}
	}
}
