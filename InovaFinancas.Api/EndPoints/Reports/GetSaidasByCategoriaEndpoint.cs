using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models.Reports;
using InovaFinancas.Core.Requests.Reports;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Reports
{
	public class GetSaidasByCategoriaEndpoint : IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapGet("/saidas", HandelAsync).Produces<Response<List<SaidasByCategoria>?>>();
		}
		private static async Task<IResult> HandelAsync(ClaimsPrincipal user, 
			
			IReportHandler handler)
		{
			var request = new GetSaidasByCategoriaRequest(){
				UsuarioId = user.Identity?.Name ?? string.Empty
			};
			
			var result = await handler.GetSaidasByCategoriaReportAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

		}
	}
}
