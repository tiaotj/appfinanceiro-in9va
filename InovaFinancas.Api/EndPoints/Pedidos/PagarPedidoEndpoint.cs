using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Pedidos
{
	public class PagarPedidoEndpoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapPost("/{numero}/pagar", HandlerAsync)
				.WithName("Estornar um Pedido")
				.WithSummary("Efetura o pagamento de um pedido")
				.WithDescription("Efetura o pagamento de um pedido")
				.Produces<Response<Pedido?>>();

		}
		private static async Task<IResult> HandlerAsync(ClaimsPrincipal user, IPedidoHandler handler,
			PagamentoPedidoRequest request, string numero)
		{
			request.UsuarioId = user.Identity!.Name ?? "";	
			request.Numero = numero;
			

			var result = await handler.PagarAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
