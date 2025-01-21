using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Pedidos
{
	public partial class DetalhesPedidoPage:ComponentBase
	{
        #region Propriedades
        public bool IsBusy { get; set; }
        [Parameter]
        public string Numero { get; set; } = "";

        public Pedido Pedido { get; set; } = null!;

        #endregion

        #region Services
        [Inject]
        public IPedidoHandler Handler { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
		#endregion

		#region Metodos

		protected override async Task OnInitializedAsync()
		{
			IsBusy = true;
			try
			{
				var result = await Handler.GetByNumeroAsync(new GetPedidoByNumeroRequest { Numero = Numero });
				if (result.IsSucesso)
					Pedido = result.Data!;
				else
					Snackbar.Add(result.Mensagem!, Severity.Error);
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

		public void RefreshStatus(Pedido pedido)
		{
			Pedido = pedido;
			StateHasChanged();
		}

		#endregion
	}
}
