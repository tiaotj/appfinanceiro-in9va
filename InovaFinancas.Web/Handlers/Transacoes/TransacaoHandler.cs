using InovaFinancas.Core.Comun;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Transacao;
using InovaFinancas.Core.Responses;
using System.Net.Http.Json;

namespace InovaFinancas.Web.Handlers.Transacoes
{
	public class TransacaoHandler(IHttpClientFactory httpClientFactory) : ITransacaoHandler
	{
		private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
		public async Task<Response<Transacao?>> CreateAsync(CreateTransacaoRequest request)
		{
			var result = await _client.PostAsJsonAsync("v1/transacao",request);
			return await result.Content.ReadFromJsonAsync<Response<Transacao?>>() ??
				new Response<Transacao?>(null,400,"Falha ao criar transação");
		}

		public async Task<Response<Transacao?>> DeleteAsync(DeleteTransacaoRequest request)
		{
			var result = await _client.DeleteAsync($"v1/transacao/{request.Id}");
			return await result.Content.ReadFromJsonAsync<Response<Transacao?>>()
				?? new Response<Transacao?>(null, 400, "Falha ao excluir a transacao");
		}

		public async Task<Response<Transacao?>> GetByIdAsync(GetTransacaoByIdRequest request)
		{
			var result = await _client.GetFromJsonAsync<Response<Transacao?>>($"v1/transacao/{request.Id}");
			return result ?? new Response<Transacao?>(null, 400, "Falha ao buscar a transacao");
		}

		public async Task<PageResponse<List<Transacao>?>> GetByPeriodoAsync(GetTransacaoByPeriodoRequest request)
		{
			const string format = "yyyy-MM-dd";
			var startDate = request.StartDate is not null ? 
				request.StartDate.Value.ToString(format) : 
				DateTime.Now.GetFirstDay().ToString(format);
			
			var endDate = request.EndDate is not null ? 
				request.EndDate.Value.ToString(format) : 
				DateTime.Now.GetLastDay().ToString(format);

			var url = $"v1/transacao?startdate={startDate}&enddate={endDate}";

			return await _client.GetFromJsonAsync<PageResponse<List<Transacao>?>>(url) ??
				new PageResponse<List<Transacao>?>(null,400,"Falha ao buscar as transações por período"); 
		}

		public async Task<Response<Transacao?>> UpdateAsync(UpdateTransacaoRequest request)
		{
			var result = await _client.PutAsJsonAsync($"v1/transacao/{request.Id}", request);
			return await result.Content.ReadFromJsonAsync<Response<Transacao?>>()
				?? new Response<Transacao?>(null, 400, "Falha ao atualizar a transacao");
		}
	}
}
