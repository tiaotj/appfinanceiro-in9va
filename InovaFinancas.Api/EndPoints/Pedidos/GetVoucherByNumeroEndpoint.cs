using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Pedidos
{
	public class GetVoucherByNumeroEndpoint:IEndPoint

	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapGet("/{numero}", HandlerAsync)
				.WithName("Busca Voucher")
				.WithSummary("Buscar Voucher pelo numero")
				.WithDescription("Buscar Voucher pelo numero")
				.Produces<Response<Voucher?>>();

		}
		private static async Task<IResult> HandlerAsync(IVoucherHandler handler,
			string numero)
		{
			var request = new GetVoucherByNumeroRequest
			{
				Numero = numero
			};

			var result = await handler.GetByNumeroAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
