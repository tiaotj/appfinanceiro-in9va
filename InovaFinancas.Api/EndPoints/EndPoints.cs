using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Api.EndPoints.Categorias;
using InovaFinancas.Api.EndPoints.Pedidos;
using InovaFinancas.Api.EndPoints.Reports;
using InovaFinancas.Api.EndPoints.Stripe;
using InovaFinancas.Api.EndPoints.Transacaoes;

namespace InovaFinancas.Api.EndPoints
{
	public static class EndPoints
	{
		public static void MapEndPoints(this WebApplication app)
		{
			var endpont = app.MapGroup("");

			endpont.MapGroup("/").WithTags("Healh Check")
				.MapGet("/", () => new { message = "Helo World" });

			endpont.MapGroup("v1/categoria")
				.WithTags("categoria")
				.RequireAuthorization()
				.MapEndepoint<CreateCategoriaEndPoint>()
				.MapEndepoint<UpdateCategoriaEndPoint>()
				.MapEndepoint<DeleteCategoriaEndPoint>()
				.MapEndepoint<GetCategoriaByIdEndPoint>()
				.MapEndepoint<GetAllCategoriaEndPoint>();

			endpont.MapGroup("v1/transacao")
				.WithTags("transcao")
				.RequireAuthorization()
				.MapEndepoint<CreateTransacaoEndPoint>()
				.MapEndepoint<UpdateTransacaoEndPoint>()
				.MapEndepoint<DeleteTransacaoEndPoint>()
				.MapEndepoint<GetTransacaoByIdEndPoint>()
				.MapEndepoint<GetTransacaoByPeriodoEndPoint>();

			endpont.MapGroup("v1/reports")
				.WithTags("Reports")
				.RequireAuthorization()
				.MapEndepoint<GetEntradasByCategoriaEndpoint>()
				.MapEndepoint<GetEntradasSaidasEndpoint>()
				.MapEndepoint<GetFinancasSummaryEndpoint>()
				.MapEndepoint<GetSaidasByCategoriaEndpoint>();

			endpont.MapGroup("v1/produto")
				.WithTags("Produto")
				.RequireAuthorization()
				.MapEndepoint<GetAllProdutosEndpoint>()
				.MapEndepoint<GetProdutoBySlugEndpoint>();
				


			endpont.MapGroup("v1/voucher")
			.WithTags("Voucher")
			.RequireAuthorization()
			.MapEndepoint<GetVoucherByNumeroEndpoint>();
			
			endpont.MapGroup("v1/pedido")
			.WithTags("Pedido")
			.RequireAuthorization()
			.MapEndepoint<GetPedidoByNumeroEndpoint>()
			.MapEndepoint<GetAllPedidosEndpoint>()
			.MapEndepoint<CreatePedidoEndPoint>()
			.MapEndepoint<PagarPedidoEndpoint>()
			.MapEndepoint<CancelarPedidoEndpoint>()
			.MapEndepoint<EstornarPagamentoPedidoEndpoint>();


			endpont.MapGroup("v1/pagamento/stripe")
			.WithTags("pagamento - stripe")
			.RequireAuthorization()
			.MapEndepoint<CreateSessionEndpoint>();
		}



		private static IEndpointRouteBuilder MapEndepoint<TEndPoint>(this IEndpointRouteBuilder app) where TEndPoint : IEndPoint
		{
			TEndPoint.Map(app);
			return app;
		}
	}
}
