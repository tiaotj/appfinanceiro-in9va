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
	public class GetAllProdutosEndpoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapGet("/", HandlerAsync)
				.WithName("Busca Produto")
				.WithSummary("Buscar todos os Produtos")
				.WithDescription("Buscar todos os Produtos")
				.Produces<PageResponse<List<Produto>?>>();

		}
		private static async Task<IResult> HandlerAsync(IProdutoHandler handler,
			[FromQuery] int NumeroPagina = Configuration.NumeroPagina,
			[FromQuery] int TamanhoPagina = Configuration.DefaultTamanhoPagina)
		{
			var request = new GetAllProdutosRequest
			{
				NumeroPagina = NumeroPagina,
				TamanhoPagina = TamanhoPagina
			};

			var result = await handler.GetAllAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
