using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Pedidos
{
	public class CreatePedidoEndPoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapPost("/", HandlerAsync)
				.WithName("Criar Pedido")
				.WithSummary("Cria um pedido")
				.WithDescription("Cia um pedido")
				.Produces<Response<Pedido?>>();

		}
		private static async Task<IResult> HandlerAsync(IPedidoHandler handler, CreatePedidoRequest request, ClaimsPrincipal user)
		{
			request.UsuarioId = user.Identity!.Name ?? string.Empty;

			var result = await handler.CreateAsync(request);
			return result.IsSucesso ? TypedResults.Created($"v1/pedido/{result.Data?.Numero}",result) : TypedResults.BadRequest(result);
		}
	}
}
