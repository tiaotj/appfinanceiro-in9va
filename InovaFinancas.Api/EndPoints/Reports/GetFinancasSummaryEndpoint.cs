using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models.Reports;
using InovaFinancas.Core.Requests.Reports;
using InovaFinancas.Core.Responses;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Reports
{
	public class GetFinancasSummaryEndpoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		{
			app.MapGet("/summary", HandelAsync).Produces<Response<FinancasSummary?>>();
		}
		private static async Task<IResult> HandelAsync(ClaimsPrincipal user,
	
			IReportHandler handler)
		{
			var request = new GetFinancasSummaryRequest
			{
				UsuarioId = user.Identity?.Name ?? string.Empty
			};
			
			var result = await handler.GetFinancasSummaryReportAsync(request);
			return result.IsSucesso ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

		}
	}
}
