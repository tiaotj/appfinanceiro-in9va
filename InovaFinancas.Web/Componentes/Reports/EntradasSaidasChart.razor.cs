using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Componentes.Reports
{
	public partial class EntradasSaidasChartPage : ComponentBase
	{
		#region Propriedades
		public ChartOptions Options { get; set; } = new();
		public List<ChartSeries> Series { get; set; } = [];
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
			var result = await Handler.GetEntradasSaidasReportAsync(new GetEntradasSaidasRequest());
			if (!result.IsSucesso || result.Data is null)
			{
				Snackbar.Add("Falha o buscar dados");
				return;
			}
			else
			{
				var entradas = new List<double>();	
				var saidas = new List<double>();

				foreach (var item in result.Data) {

					entradas.Add((double)item.Entradas);
					saidas.Add((double)item.Saidas);
					Labels.Add(GetMesNome(item.Mes));
				}

				Options.YAxisTicks = 1000;
				Options.LineStrokeWidth = 5;

				Options.ChartPalette = [Colors.Green.Default, Colors.Red.Default];
				Series = [new ChartSeries{Name = "Entradas", Data = entradas.ToArray()},
						  new ChartSeries{Name = "Saídas", Data = saidas.ToArray()}];

				StateHasChanged();
			}
		}
		private string GetMesNome(int mes)
		{
			return new DateTime(2024, mes, 1).ToString("MMM");
		}
		#endregion

	}
}
