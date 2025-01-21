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
	public class GetAllPedidosEndpoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapGet("/", HandlerAsync)
				.WithName("Busca Pedidos")
				.WithSummary("Buscar todos os pedidos")
				.WithDescription("Buscar todos os pedidos")
				.Produces<PageResponse<List<Pedido>?>>();

		}
		private static async Task<IResult> HandlerAsync(ClaimsPrincipal user, IPedidoHandler handler, 
			[FromQuery] int NumeroPagina = Configuration.NumeroPagina, 
			[FromQuery] int TamanhoPagina = Configuration.DefaultTamanhoPagina)
		{
			var request = new GetAllPedidosRequest
			{
				UsuarioId = user.Identity!.Name ?? "",
				NumeroPagina = NumeroPagina,
				TamanhoPagina = TamanhoPagina
			};

			var result = await handler.GetAllAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
