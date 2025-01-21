using InovaFinancas.Core.Models.Reports;
using InovaFinancas.Core.Requests.Reports;
using InovaFinancas.Core.Responses;

namespace InovaFinancas.Core.Handlers
{
	public interface IReportHandler
	{
		Task<Response<List<EntradasSaidas>?>> GetEntradasSaidasReportAsync(GetEntradasSaidasRequest request);
		Task<Response<List<EntradasByCategoria>?>> GetEntradasByCategoriaReportAsync(GetEntradasByCategoriaRequest request);
		Task<Response<List<SaidasByCategoria>?>> GetSaidasByCategoriaReportAsync(GetSaidasByCategoriaRequest request);
		Task<Response<FinancasSummary?>> GetFinancasSummaryReportAsync(GetFinancasSummaryRequest request);
	}
}
