using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Transacao;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Transacaoes
{
	public class GetTransacaoByIdEndPoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		=> app.MapGet("/{id}", HandleAsync)
		.WithName("Transacao: Listar")
		.WithSummary("Lista as Transacao")
		.WithDescription("lista as  Transacao")
		.Produces<Response<Transacao?>>();

		private static async Task<IResult> HandleAsync(ITransacaoHandler handler, ClaimsPrincipal user, long id)
		{


			var request = new GetTransacaoByIdRequest()
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
