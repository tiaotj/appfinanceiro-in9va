using InovaFinancas.Core.Comun;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Transacao;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Transacoes
{
	public partial class ListTranscaoPage: ComponentBase
	{
		#region Propriedades
		public bool IsBusy { get; set; } = false;
		public List<Transacao> Transacaos { get; set; } = [];
		public string TextoPesquisa { get; set; } = string.Empty;
        public int AnoAtual { get; set; } = DateTime.Now.Year;
		public int MesAtual { get; set; } = DateTime.Now.Month;
		public int[] Anos { get; set; } =
			{DateTime.Now.Year,
			DateTime.Now.AddYears(-1).Year,
			DateTime.Now.AddYears(-2).Year,
			DateTime.Now.AddYears(-3).Year,
			DateTime.Now.AddYears(-4).Year,
			DateTime.Now.AddYears(-5).Year,

		};
        #endregion

        #region Servicos
        [Inject]
		public ITransacaoHandler Handler { get; set; } = null!;
		[Inject]
		public NavigationManager NavigationManager { get; set; } = null!;
		[Inject]
		public ISnackbar Snackbar { get; set; } = null!;
		[Inject]
		public IDialogService DialogService { get; set; } = null!;

		#endregion

		#region Metodos

		public async Task GetTransacoesAsync()
		{
			IsBusy = true;
			try
			{
				var request = new GetTransacaoByPeriodoRequest()
				{
					StartDate = DateTime.Now.GetFirstDay(AnoAtual,MesAtual),
					EndDate = DateTime.Now.GetLastDay(AnoAtual,MesAtual),
					NumeroPagina = 1,
					TamanhoPagina=25
				};
				var result = await Handler.GetByPeriodoAsync(request);
				if (result.IsSucesso)
				{
					Transacaos = result.Data ?? [];
				}

			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
			finally { IsBusy = false; }

		}
		public Func<Transacao, bool> Filter => x =>
		{

			if (string.IsNullOrEmpty(TextoPesquisa))
				return true;

			if (x.Id.ToString().Contains(TextoPesquisa, StringComparison.OrdinalIgnoreCase))
				return true;

			if (x.Titulo.Contains(TextoPesquisa, StringComparison.OrdinalIgnoreCase))
				return true;
			
			if (x.Valor == decimal.Parse(TextoPesquisa))
				return true;

			
			return false;
		};


		public async void OnDeleteButtonClickedAsync(long id, string titulo)
		{
			var result = await DialogService.ShowMessageBox("A T E N Ç Ã O", $"Deseja exlcuir a Transacao {titulo}, isso é irrevesível", yesText: "Excluir", cancelText: "Cancelar");


			if (result is true)
				await OnDeleteAsync(id, titulo);

			//Atualiza a lista sem ir no banco
			StateHasChanged();
		}

		public async Task OnDeleteAsync(long id, string titulo)
		{
			try
			{
				var request = new DeleteTransacaoRequest() { Id = id };
				var result = await Handler.DeleteAsync(request);
				Transacaos.RemoveAll(x => x.Id == id);
				Snackbar.Add($"Transacao: {titulo} Excluida", Severity.Success);
				//Atualiza a lista sem ir no banco
				StateHasChanged();
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
		}

		#endregion

		#region Overrides

		protected override async Task OnInitializedAsync() => await GetTransacoesAsync();
		
		#endregion
	}
}
