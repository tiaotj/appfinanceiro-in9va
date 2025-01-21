using InovaFinancas.Core.Requests.Stripe;
using InovaFinancas.Core.Responses;
using InovaFinancas.Core.Responses.Stripe;

namespace InovaFinancas.Core.Handlers
{
	public interface IStripeHandler
	{
		public Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request);
		public Task<Response<List<StripeTransactionResponse>>> GetTransactonByPedidoNumeroAsync(GetTransactionByPedidoNumeroRequest request);
	}
}
