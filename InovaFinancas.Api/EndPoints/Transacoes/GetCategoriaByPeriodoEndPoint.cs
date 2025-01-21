using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Core;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Transacao;
using InovaFinancas.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InovaFinancas.Api.EndPoints.Transacaoes
{
	public class GetTransacaoByPeriodoEndPoint:IEndPoint
	{
		public static void Map(IEndpointRouteBuilder app)
		=> app.MapGet("/", HandleAsync)
		.WithName("Transacao: Listar All")
		.WithSummary("Lista as Transacao All por data")
		.WithDescription("lista as  Transacao All por data")
		.Produces<PageResponse<List<Transacao?>>>();

		private static async Task<IResult> HandleAsync(ITransacaoHandler handler,
			ClaimsPrincipal user,
			[FromQuery] DateTime? startdate = null,
			[FromQuery] DateTime? enddate = null,
			[FromQuery] int tamanhoPagina = Configuration.DefaultTamanhoPagina, 
			[FromQuery] int numeroPagina = Configuration.NumeroPagina)
		{

			var request = new GetTransacaoByPeriodoRequest()
			{
				UsuarioId = user.Identity?.Name ?? string.Empty,
				TamanhoPagina = tamanhoPagina,
				NumeroPagina = numeroPagina,
				StartDate = startdate,
				EndDate = enddate,
			};

			
			var result = await handler.GetByPeriodoAsync(request);
			if (result.IsSucesso)
				return TypedResults.Ok(result);

			return TypedResults.BadRequest(result);
		}
	}
}
