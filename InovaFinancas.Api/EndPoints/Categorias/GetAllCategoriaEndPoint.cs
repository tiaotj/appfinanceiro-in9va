using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Categorias
{
	public class GetAllCategoriaEndPoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		=> app.MapGet("/", HandleAsync)
		.WithName("Categoria: Listar All")
		.WithSummary("Lista as Categoria All")
		.WithDescription("lista as  Categoria All")
		.Produces<PageResponse<List<Categoria?>>>();

		private static async Task<IResult> HandleAsync(ICategoriaHandler handler,
			ClaimsPrincipal user,
			[FromQuery] int tamanhoPagina = Configuration.DefaultTamanhoPagina, 
			[FromQuery] int numeroPagina = Configuration.NumeroPagina)
		{

			var request = new GetAllCategoriaRequest()
			{

				TamanhoPagina = tamanhoPagina,
				NumeroPagina = numeroPagina,
			};

			request.UsuarioId = user.Identity?.Name ?? string.Empty;
			var result = await handler.GetAllAsync(request);
			if (result.IsSucesso)
				return TypedResults.Ok(result);

			return TypedResults.BadRequest(result);
		}
	}
}
