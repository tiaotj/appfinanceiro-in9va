using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Categorias
{
	public class GetCategoriaByIdEndPoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		=> app.MapGet("/{id}", HandleAsync)
		.WithName("Categoria: Listar")
		.WithSummary("Lista as Categoria")
		.WithDescription("lista as  Categoria")
		.Produces<Response<Categoria?>>();

		private static async Task<IResult> HandleAsync(ICategoriaHandler handler, ClaimsPrincipal user, long id)
		{


			var request = new GetCategoriaByIdRequest()
			{
				Id = id,
				UsuarioId = user.Identity?.Name ?? string.Empty,
			};

			var result = await handler.GetByIdAsync(request);
			if (result.IsSucesso)
				return TypedResults.Ok(result);

			return TypedResults.BadRequest(result);
		}
	}
}
