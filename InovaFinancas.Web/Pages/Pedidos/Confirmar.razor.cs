using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Pedidos
{
	public partial class ConfirmarPage : ComponentBase
	{
		#region propriedade
		[Parameter]
		public string Numero { get; set; } = string.Empty;

		public Pedido? Pedido { get; set; }

		#endregion

		#region Services
		[Inject]
		public ISnackbar Snackbar { get; set; } = null!;
		[Inject]
		public IPedidoHandler Handler { get; set; } = null!;
		#endregion

		#region Metodos

		protected override async Task OnInitializedAsync()
		{
			var request = new PagamentoPedidoRequest
			{
				Numero = Numero,
			};
			var result = await Handler.PagarAsync(request);
			if (!result.IsSucesso)
			{
				Snackbar.Add(result.Mensagem, Severity.Error);
				return;
			}
				

			Pedido = result.Data;
			Snackbar.Add(result.Mensagem, Severity.Success);
		}
		#endregion
	}
}
