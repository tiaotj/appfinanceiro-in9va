using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Pedidos
{
	public partial class CheckoutPage: ComponentBase
	{
        #region propriedades
        public PatternMask Mask = new("####-####")
        {
            MaskChars= [new MaskChar('#',@"[0-9a-fA-F]")], 
            Placeholder='_',
            CleanDelimiters=true,
            Transformation=AllUpperCase
        };
        public bool IsBusy { get; set; } = false;
        [Parameter]
        public string slug { get; set; } = string.Empty;
        [SupplyParameterFromQuery(Name ="voucher")]
        public string? VoucherNumero { get; set; }
        public bool IsValid { get; set; }
        public CreatePedidoRequest inputModel { get; set; } = new();
        public Produto? Produto { get; set; }
        public Voucher? Voucher { get; set; }
        public decimal Total { get; set; }

        #endregion

        #region Servicos
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IPedidoHandler Handler { get; set; } = null!;
        [Inject]
        public IProdutoHandler ProdutoHandler { get; set; } = null!;
        [Inject]
        public IVoucherHandler VoucherHandler { get; set; } = null!;
        [Inject]
        public NavigationManager Navgation { get; set; } = null!;

		#endregion

		#region Metodos

		protected override async Task OnInitializedAsync()
		{
            try
            {
                var result = await ProdutoHandler.GetBySlugAsync(new GetProdutoBySlugRequest { Slug = slug });
                if (result.IsSucesso == false)
                {
                    Snackbar.Add("Não foi possível obter o produto", Severity.Error);
                    IsValid=false;
                    return;
                }

                Produto = result.Data;
            }
            catch (Exception ex)
            {
				Snackbar.Add(ex.Message, Severity.Error);
				IsValid = false;
				return;
			}

            if (Produto == null)
            {
				Snackbar.Add("Não foi possível obter o produto", Severity.Error);
				IsValid = false;
				return;
			}

            if (!string.IsNullOrEmpty(VoucherNumero)) {

                try
                {
                    var result = await VoucherHandler.GetByNumeroAsync(new GetVoucherByNumeroRequest { Numero = VoucherNumero.Replace("-","") });

                    if (result.IsSucesso == false)
                    {
						VoucherNumero = "";
						Snackbar.Add("Não foi possível obter o voucher", Severity.Error);
					}

                    if (result.Data is null)
                    {
						VoucherNumero = "";
						Snackbar.Add("Não foi possível obter o voucher", Severity.Error);
					}

                    Voucher = result.Data;
                        
                }
                catch (Exception ex)
                {
					VoucherNumero = "";
					Snackbar.Add(ex.Message, Severity.Error);
				}
            }


            IsValid = true;
            Total = Produto.Valor - (Voucher?.Valor ?? 0);

		}

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var request = new CreatePedidoRequest
                {
                    ProdutoId = Produto!.Id,
                    VoucherId = Voucher?.Id ?? null
                };

                var result = await Handler.CreateAsync(request);
                if (result.IsSucesso)
                    Navgation.NavigateTo($"/pedido/{result.Data!.Numero}");
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

		#endregion

        private static char AllUpperCase(char c)=> c.ToString().ToUpperInvariant()[0];
	}
}
