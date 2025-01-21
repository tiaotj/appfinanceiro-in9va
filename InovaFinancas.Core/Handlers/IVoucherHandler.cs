using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;

namespace InovaFinancas.Core.Handlers
{
	public interface IVoucherHandler
	{
		Task<Response<Voucher?>> GetByNumeroAsync(GetVoucherByNumeroRequest request);
	}
}
