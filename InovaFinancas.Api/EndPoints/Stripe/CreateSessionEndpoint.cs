using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Requests.Stripe;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Stripe
{
	public class CreateSessionEndpoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapPost("/session", HandlerAsync).Produces<string?>();
		}

		private static async Task<IResult> HandlerAsync(ClaimsPrincipal user, IStripeHandler handler, CreateSessionRequest request)
		{
			request.UsuarioId = user.Identity!.Name ?? string.Empty;
			var result  = await handler.CreateSessionAsync(request);
			 return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
		}
	}
}
