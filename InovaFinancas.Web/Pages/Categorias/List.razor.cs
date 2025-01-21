using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Categorias
{
	public partial class ListCategoriaPage : ComponentBase
	{
		#region Propriedades
		public bool IsBusy { get; set; } = false;
		public List<Categoria> Categorias { get; set; } = [];
		public string TextoPesquisa { get; set; } = string.Empty;
		#endregion

		#region Servicos
		[Inject]
		public ICategoriaHandler Handler { get; set; } = null!;
		[Inject]
		public NavigationManager NavigationManager { get; set; } = null!;
		[Inject]
		public ISnackbar Snackbar { get; set; } = null!;
		[Inject]
		public IDialogService DialogService { get; set; } = null!;

		#endregion

		#region Metodos
		public Func<Categoria, bool> Filter => x =>
		{

			if (string.IsNullOrEmpty(TextoPesquisa))
				return true;

			if (x.Id.ToString().Contains(TextoPesquisa, StringComparison.OrdinalIgnoreCase))
				return true;

			if (x.Titulo.Contains(TextoPesquisa, StringComparison.OrdinalIgnoreCase))
				return true;

			if (x.Descricao is not null && x.Descricao.Contains(TextoPesquisa, StringComparison.OrdinalIgnoreCase))
				return true;

			return false;
		};


		public async void OnDeleteButtonClickedAsync(long id, string titulo)
		{
			var result = await DialogService.ShowMessageBox("A T E N Ç Ã O", $"Deseja exlcuir a categoria {titulo}, isso é irrevesível", yesText: "Excluir", cancelText: "Cancelar");


			if (result is true)
				await OnDeleteAsync(id, titulo);

			//Atualiza a lista sem ir no banco
			StateHasChanged();
		}

		public async Task OnDeleteAsync(long id, string titulo)
		{
			try
			{
				var request = new DeleteCategoriaRequest() { Id = id };
				var result = await Handler.DeleteAsync(request);
				Categorias.RemoveAll(x => x.Id == id);
				Snackbar.Add($"Categoria: {titulo} Excluida", Severity.Success);
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
		}

		#endregion

		#region Overrides

		protected override async Task OnInitializedAsync()
		{
			IsBusy = true;
			try
			{
				var request = new GetAllCategoriaRequest();
				var result = await Handler.GetAllAsync(request);
				if (result.IsSucesso)
					Categorias = result.Data ?? [];
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
	}
}
