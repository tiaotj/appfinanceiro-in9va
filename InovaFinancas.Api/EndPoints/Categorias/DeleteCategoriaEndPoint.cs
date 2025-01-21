using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Categorias
{
	public class DeleteCategoriaEndPoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		=> app.MapDelete("/{id}", HandleAsync)
		.WithName("Categoria: Excluir")
		.WithSummary("Exlcui uma Categoria")
		.WithDescription("Exclui Categoria")
		.Produces<Response<Categoria?>>();

		private static async Task<IResult> HandleAsync(ICategoriaHandler handler, ClaimsPrincipal user, long id)
		{

			var request = new DeleteCategoriaRequest()
			{
				UsuarioId = user.Identity?.Name ?? string.Empty,
				Id = id
			};
			request.Id = id;
			request.UsuarioId = "Tiao";
			var result = await handler.DeleteAsync(request);
			if (result.IsSucesso)
				return TypedResults.Ok(result);

			return TypedResults.BadRequest(result);
		}
	}
}
