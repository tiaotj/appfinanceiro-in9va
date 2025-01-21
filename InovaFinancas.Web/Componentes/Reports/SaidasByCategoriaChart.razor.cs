using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Componentes.Reports
{
	public partial class SaidasByCategoriaChartPage:ComponentBase
	{
		#region Propriedades

		public List<double> Data { get; set; } = [];
		public List<string> Labels { get; set; } = [];
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
			await GetSaidasByCategoriaAsyn();
		}
		private async Task GetSaidasByCategoriaAsyn()
		{
			var result = await Handler.GetSaidasByCategoriaReportAsync(new GetSaidasByCategoriaRequest());
			if (!result.IsSucesso || result.Data is null)
			{
				Snackbar.Add("Falha o buscar dados");
				return;
			}
			foreach (var item in result.Data)
			{
				Labels.Add($"{item.Categoria.ToString()} ({item.Despesa:C})");
				Data.Add(-(double)item.Despesa);

			}

		}
		#endregion
	}
}
