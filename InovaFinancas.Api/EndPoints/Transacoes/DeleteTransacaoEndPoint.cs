using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Transacao;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Transacaoes
{
	public class DeleteTransacaoEndPoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		=> app.MapDelete("/{id}", HandleAsync)
		.WithName("Transacao: Excluir")
		.WithSummary("Exlcui uma Transacao")
		.WithDescription("Exclui Transacao")
		.Produces<Response<Transacao?>>();

		private static async Task<IResult> HandleAsync(ITransacaoHandler handler, ClaimsPrincipal user, long id)
		{

			var request = new DeleteTransacaoRequest()
			{
				UsuarioId = user.Identity?.Name ?? string.Empty,
				Id = id
			};
			
			var result = await handler.DeleteAsync(request);
			if (result.IsSucesso)
				return TypedResults.Ok(result);

			return TypedResults.BadRequest(result);
		}
	}
}
