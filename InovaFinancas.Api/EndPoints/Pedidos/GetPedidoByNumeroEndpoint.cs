using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Pedidos
{
	public class GetPedidoByNumeroEndpoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapGet("/{numero}", HandlerAsync)
				.WithName("Busca Pedidos por id")
				.WithSummary("Buscar pedidos pelo numero")
				.WithDescription("Buscar pedidos pelo numero")
				.Produces<Response<Pedido?>>();

		}
		private static async Task<IResult> HandlerAsync(ClaimsPrincipal user, IPedidoHandler handler,
			string numero)
		{
			var request = new GetPedidoByNumeroRequest
			{
				UsuarioId = user.Identity!.Name ?? "",
				Numero = numero
			};

			var result = await handler.GetByNumeroAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
