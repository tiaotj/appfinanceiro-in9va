using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models.Reports;
using InovaFinancas.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Componentes.Reports
{
	public partial class ResumoFinanceiroChartpag:ComponentBase
	{
        #region Propriedades
        public bool ShowValues { get; set; } = true;
        public FinancasSummary? Resumo { get; set; }
		#endregion
		#region Services
		[Inject]
        public IReportHandler Handler { get; set; } = null!;
		[Inject]
        public ISnackbar Snackbar { get; set; } = null!;
		#endregion
		#region Overrides
		protected override async Task OnInitializedAsync()
		{
			var result = await Handler.GetFinancasSummaryReportAsync(new GetFinancasSummaryRequest());
			if (result.IsSucesso)
			{
				Resumo = result.Data;
			}
		}

		public void AlternaShowValues() => ShowValues = !ShowValues;

		#endregion
	}
}
