using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Pedidos
{
	public class EstornarPagamentoPedidoEndpoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapPost("/{id}/estorno", HandlerAsync)
				.WithName("Pagar um Pedido")
				.WithSummary("Efetura o pagamento de um pedido")
				.WithDescription("Efetura o pagamento de um pedido")
				.Produces<Response<Pedido?>>();

		}
		private static async Task<IResult> HandlerAsync(ClaimsPrincipal user, IPedidoHandler handler,
			EstornoPedidoRequest request, long id)
		{
			request.UsuarioId = user.Identity!.Name ?? "";
			request.PedidoId = id;


			var result = await handler.EstornarAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
