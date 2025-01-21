using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Requests.Stripe;
using InovaFinancas.Web.Pages.Pedidos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace InovaFinancas.Web.Componentes.Pedidos
{
	public partial class PedidoAcoesComponet : ComponentBase
	{
		#region Paramentos
		[CascadingParameter]
		public DetalhesPedidoPage PagePai { get; set; } = null!;
		public bool IsBusy { get; set; }

		[Parameter, EditorRequired]
		public Pedido Pedido { get; set; } = null!;
		#endregion
		#region Services
		[Inject]
		public IDialogService DialogService { get; set; } = null!;
		[Inject]
		public IPedidoHandler PedidoHandler { get; set; } = null!;
		[Inject]
		public ISnackbar Snackbar { get; set; } = null!;
		[Inject]
		public IStripeHandler StripHandler { get; set; } = null!;
		[Inject]
		public IJSRuntime jSRuntime { get; set; } = null!;
        #endregion
        #region Metodos
        public async Task OnCancelarPedidoAsync()
		{
			bool? resultM = await DialogService.ShowMessageBox("ATENÇÃO", "Deseja relamente cancelar este pedido?", yesText: "SIM", noText: "NÃO");

			if (resultM is null)
				return;

			if (resultM == false)
				return;

			await CancelarPedido();
		}
		public async void OnPagarPedidoAsync()
		{
			await PagarPedidoAsync();
		}

		public async Task OnEstornarPedidoAsync()
		{
			bool? resultM = await DialogService.ShowMessageBox("ATENÇÃO", "Deseja relamente solicitar o estorno deste pedido?", yesText: "SIM", noText: "NÃO");

			if (resultM is null)
				return;

			if (resultM == false)
				return;

			await EstornarPedido();
		}

		private async Task CancelarPedido()
		{
			IsBusy = true;
			try
			{
				var result = await PedidoHandler.CancelarAsync(new CancelarPedidoRequest { Id = Pedido.Id });
				if (result.IsSucesso)
				{
					Pedido = result.Data!;
					Snackbar.Add($"Pedido {Pedido.Numero} Cancelado", Severity.Info);
					PagePai.RefreshStatus(Pedido);
				}
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

		private async Task EstornarPedido()
		{
			IsBusy = true;
			try
			{
				var result = await PedidoHandler.EstornarAsync(new EstornoPedidoRequest { PedidoId = Pedido.Id });
				if (result.IsSucesso)
				{
					Pedido = result.Data!;
					Snackbar.Add($"Pedido {Pedido.Numero} Estornado", Severity.Info);
					PagePai.RefreshStatus(Pedido);
				}
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
		private async Task PagarPedidoAsync()
		{
			IsBusy= true;
			var request = new CreateSessionRequest
			{
				PedidoNumero = Pedido.Numero,
				Total = (long)Math.Round(Pedido.Total * 100, 2),
				ProdutoTitulo = Pedido.Produto.Titulo,
				ProdutoDescricao = Pedido.Produto.Descricao
			};
			try
			{
				var result = await StripHandler.CreateSessionAsync(request);
				if (!result.IsSucesso)
				{
					Snackbar.Add(result.Mensagem, Severity.Error);
					return;
				}

				if (result.Data is null)
				{
					Snackbar.Add(result.Mensagem, Severity.Error);
					return;
				}

				await jSRuntime.InvokeVoidAsync("checkout", Configuration.StripePublicKey, result.Data);

			}
			catch(Exception ex) 
			{
				var x = ex.Message; 
				Snackbar.Add("Não foi possível inicar a seção como stripe", Severity.Error);
			}
			finally { IsBusy = false; }

			
		}
		#endregion

	}
}
