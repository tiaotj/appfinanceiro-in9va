using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Transacao;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Transacaoes
{
	public class UpdateTransacaoEndPoint: IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		=> app.MapPut("/{id}", HandleAsync)
		.WithName("Transacao: Atualização")
		.WithSummary("Atualiza nova Transacao")
		.WithDescription("Atualiza nova Transacao")
		.Produces<Response<Transacao?>>();

		private static async Task<IResult> HandleAsync(ITransacaoHandler handler, ClaimsPrincipal user, UpdateTransacaoRequest request, long id	)
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
