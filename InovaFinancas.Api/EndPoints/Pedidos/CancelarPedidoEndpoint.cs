using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Pedidos
{
	public class CancelarPedidoEndpoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapPost("/{id}/cancelar",HandlerAsync)
				.WithName("Cancelar Pedido")
				.WithSummary("Cancelar um pedido")
				.WithDescription("Cancela um pedido")
				.Produces<Response<Pedido?>>();

		}
		private static async Task<IResult> HandlerAsync(IPedidoHandler handler, long id,ClaimsPrincipal user)
		{
			var request = new CancelarPedidoRequest
			{
				Id = id,
				UsuarioId = user.Identity!.Name ?? string.Empty
			};

			var result = await handler.CancelarAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
