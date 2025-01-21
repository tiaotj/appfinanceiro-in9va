using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models.Reports;
using InovaFinancas.Core.Requests.Reports;
using InovaFinancas.Core.Responses;
using System.Net.Http.Json;
using System.Security.AccessControl;

namespace InovaFinancas.Web.Handlers
{
	public class ReportHandler(IHttpClientFactory httpClientFactory) : IReportHandler
	{
		public readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
		public async Task<Response<List<EntradasByCategoria>?>> GetEntradasByCategoriaReportAsync(GetEntradasByCategoriaRequest request)
		{
			var result = await _client.GetFromJsonAsync<Response<List<EntradasByCategoria>?>>("v1/reports/entradas");
			return result ?? new Response<List<EntradasByCategoria>?>(null, 400, "Erro ao buscar dados");
		}

		public async Task<Response<List<EntradasSaidas>?>> GetEntradasSaidasReportAsync(GetEntradasSaidasRequest request)
		{
			var result = await _client.GetFromJsonAsync<Response<List<EntradasSaidas>?>>("v1/reports/entradassaidas");
			return result ?? new Response<List<EntradasSaidas>?>(null, 400, "Erro ao buscar dados");
		}

		public async Task<Response<FinancasSummary?>> GetFinancasSummaryReportAsync(GetFinancasSummaryRequest request)
		{
			var result = await _client.GetFromJsonAsync<Response<FinancasSummary?>>("v1/reports/summary");
			return result ?? new Response<FinancasSummary?>(null, 400, "Erro ao buscar dados");
		}

		public async Task<Response<List<SaidasByCategoria>?>> GetSaidasByCategoriaReportAsync(GetSaidasByCategoriaRequest request)
		{
			var result = await _client.GetFromJsonAsync< Response<List<SaidasByCategoria>?>> ("v1/reports/saidas");
			return result ?? new Response<List<SaidasByCategoria>?>(null, 400, "Erro ao buscar dados");
		}
	}
}
