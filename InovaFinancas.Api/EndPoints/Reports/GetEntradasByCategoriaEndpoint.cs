using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models.Reports;
using InovaFinancas.Core.Requests.Reports;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Reports
{
	public class GetEntradasByCategoriaEndpoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapGet("/entradas", HandelAsync).Produces<Response<List<EntradasByCategoria>?>>();
		}
		private static async Task<IResult> HandelAsync(ClaimsPrincipal user,
			
			IReportHandler handler)
		{
			var request = new GetEntradasByCategoriaRequest
			{
				UsuarioId = user.Identity?.Name ?? string.Empty
			};
			
			var result = await handler.GetEntradasByCategoriaReportAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

		}
	}
}
