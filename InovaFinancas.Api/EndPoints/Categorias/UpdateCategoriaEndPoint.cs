using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Categorias
{
	public class UpdateCategoriaEndPoint: IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		=> app.MapPut("/{id}", HandleAsync)
		.WithName("Categoria: Atualização")
		.WithSummary("Atualiza nova Categoria")
		.WithDescription("Atualiza nova Categoria")
		.Produces<Response<Categoria?>>();

		private static async Task<IResult> HandleAsync(ICategoriaHandler handler, ClaimsPrincipal user, UpdateCategoriaRequest request, long id	)
		{
			request.Id = id;
			request.UsuarioId = user.Identity?.Name ?? string.Empty;
			var result = await handler.UpdateAsync(request);
			if (result.IsSucesso)
				return TypedResults.Ok(result);

			return TypedResults.BadRequest(result);
		}
	}
}
