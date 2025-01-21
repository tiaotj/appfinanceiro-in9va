using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Produtos
{
	public partial class ListProdutoPage : ComponentBase
	{
        #region Propriedades
        public bool IsBusy { get; set; }=false;
        public List<Produto> Produtos { get; set; } = [];
		#endregion
		#region Sericies
		[Inject]
		public ISnackbar Snackbar { get; set; } = null!;
		[Inject]
		public IProdutoHandler Handler { get; set; } = null!;
		#endregion
		#region Overrides
		protected override async Task OnInitializedAsync()
		{
			IsBusy = true;
			try
			{
				var result = await Handler.GetAllAsync(new GetAllProdutosRequest());
				if (result.IsSucesso)
					Produtos = result.Data ?? [];
				else
					Snackbar.Add(result.Mensagem, Severity.Error);
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
			finally {
				IsBusy = false;
			}
		}
		#endregion
	}
}
