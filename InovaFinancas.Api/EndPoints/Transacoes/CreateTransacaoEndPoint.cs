using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Requests.Transacao;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Transacaoes
{
	public class CreateTransacaoEndPoint: IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
			=> app.MapPost("/", HandleAsync)
			.WithName("Transação: Criação")
			.WithSummary("Cria uma nova Transação")
			.WithDescription("Cria uma nova Transação")
			.Produces<Response<Transacao?>>();


		private static async Task<IResult> HandleAsync(ITransacaoHandler handler, ClaimsPrincipal user, CreateTransacaoRequest request)
		{
			request.UsuarioId = user.Identity?.Name ?? string.Empty;
			var result = await handler.CreateAsync(request);
			if (result.IsSucesso)
				return TypedResults.Created($"/{result.Data?.Id}", result);

			return TypedResults.BadRequest(result);
		}
	}
}
