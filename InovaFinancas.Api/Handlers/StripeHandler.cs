using InovaFinancas.Core;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Requests.Stripe;
using InovaFinancas.Core.Responses;
using InovaFinancas.Core.Responses.Stripe;
using Microsoft.AspNetCore.Mvc.Formatters;
using Stripe;
using Stripe.Checkout;

namespace InovaFinancas.Api.Handlers
{
	public class StripeHandler : IStripeHandler
	{
		public async Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request)
		{
			try
			{
				var options = new SessionCreateOptions()
				{
					CustomerEmail = request.UsuarioId,
					PaymentIntentData = new SessionPaymentIntentDataOptions
					{
						Metadata = new Dictionary<string, string> { { "pedido", request.PedidoNumero } }
					},
					PaymentMethodTypes = ["card"],
					LineItems = [new SessionLineItemOptions {
						PriceData = new SessionLineItemPriceDataOptions{
							ProductData = new SessionLineItemPriceDataProductDataOptions{
								Name = request.ProdutoTitulo,
								Description = request.ProdutoDescricao,
							},
							UnitAmount = request.Total,
							Currency = "BRL"
						},
						Quantity = 1

					} ],
					Mode = "payment",
					SuccessUrl = $"{Configuration.FronEndUrl}/pedido/{request.PedidoNumero}/confirmar",
					CancelUrl = $"{Configuration.FronEndUrl}/pedido/{request.PedidoNumero}/cancelar"
				};
				var services = new SessionService();
				var session = await services.CreateAsync(options);

				return new Response<string?>(session.Id);
			}
			catch (Exception ex)
			{
				return new Response<string?>(null, 500, ex.Message);
			}
		}

		public async Task<Response<List<StripeTransactionResponse>>> GetTransactonByPedidoNumeroAsync(GetTransactionByPedidoNumeroRequest request)
		{
			var options = new ChargeSearchOptions()
			{
				Query = $"metadata['pedido']:'{request.Numero}'"
			};
			var services = new ChargeService();
			var result = await services.SearchAsync(options);

			if (result.Data.Count == 0)
				return new Response<List<StripeTransactionResponse>>(null, 404, "Nenhuma transação encontrada");

			var data = new List<StripeTransactionResponse>();
			foreach (var item in result.Data)
			{
				data.Add(new StripeTransactionResponse
				{
					Id = item.Id,
					Email = item.BillingDetails.Email,
					Valor = item.Amount,
					ValorCapturado = item.AmountCaptured,
					Status = item.Status,
					Pago = item.Paid,
					Estornado = item.Refunded,
				});

			}

			return new Response<List<StripeTransactionResponse>>(data);


		}
	}
}
