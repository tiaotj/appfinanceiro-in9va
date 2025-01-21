using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Requests.Categorias;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Formats.Asn1;

namespace InovaFinancas.Web.Pages.Categorias
{
	public partial class EditarPage : ComponentBase
	{
		#region Propriedades
		public bool IsBusy { get; set; } = false;
		[Parameter]
		public string Id { get; set; }
		public UpdateCategoriaRequest inputModel { get; set; } = new();
		#endregion

		#region Servicos
		[Inject]
		public ICategoriaHandler Handler { get; set; } = null!;
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
				var result = await Handler.UpdateAsync(inputModel);
				if (result.IsSucesso)
				{
					Snackbar.Add($"Categoria {inputModel.Titulo} Atualizida", Severity.Success);
					NavigationManager.NavigateTo("/categorias");
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
		protected override async Task OnInitializedAsync()
		{
			IsBusy = true;
			try
			{
				var request = new GetCategoriaByIdRequest()
				{
					Id = long.Parse(Id)
				};

				var result = await Handler.GetByIdAsync(request);
				if (result.IsSucesso && result.Data is not null)
				{
					inputModel = new UpdateCategoriaRequest()
					{
						Id = result.Data.Id,
						Titulo = result.Data.Titulo,
						Descricao = result.Data.Descricao
					};
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
