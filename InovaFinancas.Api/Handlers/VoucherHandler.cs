using InovaFinancas.Api.Data;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InovaFinancas.Api.Handlers
{
	public class VoucherHandler(AppDbConext context) : IVoucherHandler
	{
        public async Task<Response<Voucher?>> GetByNumeroAsync(GetVoucherByNumeroRequest request)
		{
			try
			{
				var voucher = await context.Voucheres.AsNoTracking().FirstOrDefaultAsync(x => x.Numero == request.Numero && x.IsAtivo);
				return voucher is null ?
					new Response<Voucher?>(null, 404, "Voucher não encontrado") :
					new Response<Voucher?>(voucher);
			}
			catch (Exception)
			{
				return new Response<Voucher?>(null, 500, "Não foi possível recuperar Voucher");
			}
		}
	}
}
