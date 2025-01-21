using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Pedidos
{
	public class GetProdutoBySlugEndpoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapGet("/{slug}", HandlerAsync)
				.WithName("Busca produto")
				.WithSummary("Buscar produto pelo slug")
				.WithDescription("Buscar produto pelo slug")
				.Produces<Response<Produto?>>();

		}
		private static async Task<IResult> HandlerAsync(IProdutoHandler handler,
			string slug)
		{
			var request = new GetProdutoBySlugRequest
			{
				Slug = slug
			};

			var result = await handler.GetBySlugAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
