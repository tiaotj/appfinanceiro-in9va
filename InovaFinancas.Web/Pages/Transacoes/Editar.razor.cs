using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Requests.Transacao;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Transacoes
{
	public partial class EditarTransacaoPage:ComponentBase
	{
		#region Propriedades
		public bool IsBusy { get; set; } = false;
		[Parameter]
		public string Id { get; set; }
		public UpdateTransacaoRequest inputModel { get; set; } = new();

		public List<Categoria> Categorias { get; set; } = new();
		#endregion

		#region Servicos
		[Inject]
		public ITransacaoHandler Handler { get; set; } = null!;
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
				var result = await Handler.UpdateAsync(inputModel);
				if (result.IsSucesso)
				{
					Snackbar.Add($"Transacao {inputModel.Titulo} Atualizida", Severity.Success);
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

		public async void CarregaCategorias()
		{
		
			IsBusy = true;
			try
			{
				var request = new GetAllCategoriaRequest();
				var result = await CategoriaHandler.GetAllAsync(request);
				if (result.IsSucesso)
				{
					Categorias = result.Data ?? [];					
				}

			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
			finally { IsBusy = false; }
		}

		#endregion

		#region Overrides
		protected override async Task OnInitializedAsync()
		{
			IsBusy = true;

			CarregaCategorias();

			try
			{
				var request = new GetTransacaoByIdRequest()
				{
					Id = long.Parse(Id)
				};

				var result = await Handler.GetByIdAsync(request);
				if (result.IsSucesso && result.Data is not null)
				{
					inputModel = new UpdateTransacaoRequest()
					{
						Id = result.Data.Id,
						Titulo = result.Data.Titulo,
						Valor = result.Data.Valor,
						CategoriaId = result.Data.CategoriaId,
						DataRecebimento = result.Data.DataRecebimento,
						Tipo = result.Data.Tipo,
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
