using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Categorias
{
	public class CreateCategoriaEndPoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
			=> app.MapPost("/", HandleAsync)
			.WithName("Categoria: Criação")
			.WithSummary("Cria uma nova Categoria")
			.WithDescription("Cria uma nova Categoria")
			.Produces<Response<Categoria?>>();


		private static async Task<IResult> HandleAsync(ICategoriaHandler handler, ClaimsPrincipal user, CreateCategoriaRequest request)
		{

			request.UsuarioId = user.Identity?.Name ?? string.Empty;
			var result = await handler.CreateAsync(request);
			if (result.IsSucesso)
				return TypedResults.Created($"/{result.Data?.Id}", result);

			return TypedResults.BadRequest(result);
		}




	}
}
