using InovaFinancas.Api.Data;
using InovaFinancas.Core.Enums;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models.Reports;
using InovaFinancas.Core.Requests.Reports;
using InovaFinancas.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InovaFinancas.Api.Handlers
{
	public class ReportHandler(AppDbConext context) : IReportHandler
	{
		public async  Task<Response<List<EntradasByCategoria>?>> GetEntradasByCategoriaReportAsync(GetEntradasByCategoriaRequest request)
		{
			try
			{
				var data = await context.EntradasByCategoria.AsNoTracking()
				.Where(x => x.UsuarioId == request.UsuarioId)
				.OrderByDescending(x => x.Ano)
				.ThenBy(x => x.Categoria).ToListAsync();

				return new Response<List<EntradasByCategoria>?>(data);
			}
			catch
			{
				return new Response<List<EntradasByCategoria>?>(null, 500, "Não foi possível obter dados");
			}
		}

		public async Task<Response<List<EntradasSaidas>?>> GetEntradasSaidasReportAsync(GetEntradasSaidasRequest request)
		{
			try
			{
				var data = await context.EntrasSaidas.AsNoTracking()
				.Where(x => x.UsuarioId == request.UsuarioId)
				.OrderByDescending(x => x.Ano)
				.ThenBy(x => x.Mes).ToListAsync();

				return new Response<List<EntradasSaidas>?>(data);
			}
			catch
			{
				return new Response<List<EntradasSaidas>?>(null, 500, "Não foi possível obter dados");
			}
			
		}

		public async Task<Response<FinancasSummary?>> GetFinancasSummaryReportAsync(GetFinancasSummaryRequest request)
		{
			try
			{
				var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

				var data = await context.Transacoes.AsNoTracking().
					Where(x => x.UsuarioId == request.UsuarioId &&
						x.DataRecebimento >= startDate &&
						x.DataRecebimento <= DateTime.Now).GroupBy(x=>1).Select(x=> new FinancasSummary
						(request.UsuarioId, 
						x.Where(x=>x.Tipo==ETransacaoTipo.Entrada).Sum(t=>t.Valor), 
						x.Where(x => x.Tipo == ETransacaoTipo.Saida).Sum(t => t.Valor))).FirstOrDefaultAsync();
				

				return new Response<FinancasSummary?>(data);
			}
			catch
			{
				return new Response<FinancasSummary?>(null, 500, "Não foi somar as entradas e saidas");
			}
		}

		public async Task<Response<List<SaidasByCategoria>?>> GetSaidasByCategoriaReportAsync(GetSaidasByCategoriaRequest request)
		{
			try
			{
				var data = await context.SaidasByCategoria.AsNoTracking()
				.Where(x => x.UsuarioId == request.UsuarioId)
				.OrderByDescending(x => x.Ano)
				.ThenBy(x => x.Categoria).ToListAsync();

				return new Response<List<SaidasByCategoria>?>(data);
			}
			catch
			{
				return new Response<List<SaidasByCategoria>?>(null, 500, "Não foi possível obter dados");
			}
		}
	}
}
