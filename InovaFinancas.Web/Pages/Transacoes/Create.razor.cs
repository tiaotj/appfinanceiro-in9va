using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Requests.Transacao;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Transacoes
{
	public partial class CreateTransacaoPage: ComponentBase
	{
		#region Propriedades
		public bool IsBusy { get; set; } = false;
		public CreateTransacaoRequest inputModel { get; set; } = new();
		public List<Categoria> Categorias { get; set; } = new();
        #endregion

        #region Servicos
        [Inject]
		public ITransacaoHandler TranscaoHandler { get; set; } = null!;
		[Inject]
		public ICategoriaHandler CategoriaHandler { get; set; } = null!;
        [Inject]
		public NavigationManager NavigationManager { get; set; } = null!;
		[Inject]
		public ISnackbar Snackbar { get; set; } = null!;

		#endregion

		#region Metodos
		public async Task OnValidSubmitAsync()
		{
			IsBusy = true;
			try
			{
				var result = await TranscaoHandler.CreateAsync(inputModel);
				if (result.IsSucesso)
				{
					Snackbar.Add(result.Mensagem, Severity.Success);
					NavigationManager.NavigateTo("/lancamentos/historico");
				}
				else
					Snackbar.Add(result.Mensagem, Severity.Error);

			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
			finally
			{
				IsBusy = false;
			}


		}
		#endregion

		#region Overrides

		protected override async void OnInitialized()
		{
			inputModel = new CreateTransacaoRequest();
			IsBusy = true;
			try
			{
				var request = new GetAllCategoriaRequest();
				var result = await CategoriaHandler.GetAllAsync(request);
				if (result.IsSucesso)
				{
					Categorias = result.Data ?? [];
					inputModel.CategoriaId = Categorias.FirstOrDefault()?.Id ?? 0;
				}
					
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
			finally { IsBusy = false; }	
		}
		#endregion
	}
}
