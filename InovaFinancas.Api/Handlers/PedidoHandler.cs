using InovaFinancas.Api.Data;
using InovaFinancas.Core.Enums;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Requests.Stripe;
using InovaFinancas.Core.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace InovaFinancas.Api.Handlers
{
	public class PedidoHandler(AppDbConext context,IStripeHandler stripeHandler) : IPedidoHandler
	{
		public async Task<Response<Pedido?>> CancelarAsync(CancelarPedidoRequest request)
		{
			Pedido? pedido;
			try
			{
				pedido = await context.pedidos.Include(x => x.Produto).Include(x => x.Voucher).
					   FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UsuarioId);
				if (pedido is null)
					return new Response<Pedido?>(null, 404, "Pedido não econtrado");

				switch (pedido.Estado)
				{
					case EEstadoPedido.Cancelado:
						return new Response<Pedido?>(null, 400, "Pedido já cancelado");
					case EEstadoPedido.Pago:
						return new Response<Pedido?>(null, 400, "Pedido já foi pago, faça o estorno");
					case EEstadoPedido.Estornado:
						return new Response<Pedido?>(null, 400, "Pedido já foi estornado");
					case EEstadoPedido.AguardandoPagamento:
						break;
					default:
						return new Response<Pedido?>(null, 400, "Pedido não pode ser cancelado");
				}

				pedido.Estado = EEstadoPedido.Cancelado;
				pedido.DataAlteracao = DateTime.Now;

				context.pedidos.Update(pedido);
				await context.SaveChangesAsync();

				return new Response<Pedido?>(pedido, 200, $"Pedido número:{pedido.Numero} cancelado");

			}
			catch (Exception)
			{

				return new Response<Pedido?>(null, 404, "Não foi possível buscar o pedido");
			}
		}

		public async Task<Response<Pedido?>> CreateAsync(CreatePedidoRequest request)
		{
			try
			{
				Produto? produto;
				produto = await context.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ProdutoId && x.IsAtivo);

				if (produto is null)
					return new Response<Pedido?>(null, 404, "Produto não encontrado");

				Voucher? voucher = null;
				if (request.VoucherId is not null)
				{
					voucher = await context.Voucheres.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.VoucherId && x.IsAtivo);
					if (voucher is null)
						return new Response<Pedido?>(null, 404, "Voucher não existe");

					voucher.IsAtivo = false;
					context.Voucheres.Update(voucher);
				}

				var pedido = new Pedido
				{
					UserId = request.UsuarioId,
					ProdutoId = request.ProdutoId,
					VoucherId = request.VoucherId,
				};
				context.pedidos.Add(pedido);
				await context.SaveChangesAsync();
				pedido.Voucher = voucher;
				pedido.Produto = produto;
				return new Response<Pedido?>(pedido, 201, "Pedido salvo com sucesso");

			}
			catch
			{
				return new Response<Pedido?>(null, 500, "Não foi possível criar o pedido");
			}
		}

		public async Task<Response<Pedido?>> EstornarAsync(EstornoPedidoRequest request)
		{
			Pedido? pedido;
			try
			{
				pedido = await context.pedidos.Include(x => x.Produto).Include(x => x.Voucher).
					   FirstOrDefaultAsync(x => x.Id == request.PedidoId && x.UserId == request.UsuarioId);
				if (pedido is null)
					return new Response<Pedido?>(null, 404, "Pedido não econtrado");

				switch (pedido.Estado)
				{
					case EEstadoPedido.Cancelado:
						return new Response<Pedido?>(null, 400, "Pedido já cancelado");
					case EEstadoPedido.Pago:
						break;
					case EEstadoPedido.Estornado:
						return new Response<Pedido?>(null, 400, "Pedido já foi estornado");
					case EEstadoPedido.AguardandoPagamento:
						return new Response<Pedido?>(null, 400, "Pedido ainda está em aberto");
					default:
						return new Response<Pedido?>(null, 400, "Pedido não pode ser cancelado");
				}

				pedido.Estado = EEstadoPedido.Estornado;
				pedido.DataAlteracao = DateTime.Now;

				context.pedidos.Update(pedido);
				await context.SaveChangesAsync();

				return new Response<Pedido?>(pedido, 200, $"Pedido número:{pedido.Numero} Estornado!");

			}
			catch (Exception)
			{

				return new Response<Pedido?>(null, 404, "Não foi possível buscar o pedido");
			}
		}

		public async Task<PageResponse<List<Pedido>?>> GetAllAsync(GetAllPedidosRequest request)
		{
			try
			{
				var query = context.pedidos.
					AsNoTracking()
					.Include(x => x.Produto)
					.Include(x => x.Voucher)
					.Where(x => x.UserId == request.UsuarioId)
					.OrderByDescending(x => x.DataCadastro);

				var pedido = await query.Skip(request.NumeroPagina - 1)
					.Take(request.TamanhoPagina).ToListAsync();

				var count = query.Count();
				return new PageResponse<List<Pedido>?>(pedido, count, request.NumeroPagina, request.TamanhoPagina);
			}
			catch (Exception)
			{
				return new PageResponse<List<Pedido>?>(null, 500, "Não foi possível buscar os pedidos");
			}
		}

		public async Task<Response<Pedido?>> GetByNumeroAsync(GetPedidoByNumeroRequest request)
		{
			try
			{
				var pedido = await context.pedidos.AsNoTracking().
					Include(x => x.Produto)
					.Include(x => x.Voucher).FirstOrDefaultAsync(x => x.Numero == request.Numero && x.UserId == request.UsuarioId);

				return pedido is null ? new Response<Pedido?>(null, 404, "Pedido não encontrado") :
						new Response<Pedido?>(pedido, 200, "Pedido encontrado");
			}
			catch (Exception)
			{
				return new Response<Pedido?>(null, 500, "Não foi possível buscar Pedido");
			}
		}

		public async Task<Response<Pedido?>> PagarAsync(PagamentoPedidoRequest request)
		{
			try
			{
				Pedido? pedido = null;
				pedido = await context.pedidos.Include(x=>x.Produto).Include(x => x.Voucher).
					FirstOrDefaultAsync(x => x.Numero == request.Numero && x.UserId == request.UsuarioId);

				if (pedido == null)
					return new Response<Pedido?>(null, 404, "Pedido não encontrado!");

				try
				{
					var getTransacion = new GetTransactionByPedidoNumeroRequest
					{
						Numero = pedido.Numero
					};

					var result = await stripeHandler.GetTransactonByPedidoNumeroAsync(getTransacion);

					if (!result.IsSucesso)
						return new Response<Pedido?>(null, 500, "Não foi possivel verficar o pagamento");
					if (result.Data is null)
						return new Response<Pedido?>(null, 500, "Não foi possivel verficar o pagamento");

					if (result.Data.Any(x=>x.Estornado))
						return new Response<Pedido?>(null, 500, "O pagamento desse pedido já foi estornado");

					if (!result.Data.Any(x=>x.Pago))
						return new Response<Pedido?>(null, 500, "Nenhum pagamento encontrado para esse pedido");

					request.IdPagamentoExterno = result.Data[0].Id;

				}
				catch 
				{
					return new Response<Pedido?>(null, 500, "Não foi possivel verficar o pagamento");
				}


				switch (pedido.Estado)
				{
					case EEstadoPedido.AguardandoPagamento:
						break;
					case EEstadoPedido.Pago:
						return new Response<Pedido?>(null, 400, "Esse pedido já foi pago");
					case EEstadoPedido.Cancelado:
						return new Response<Pedido?>(null, 400, "Pedido está cancelado");
					case EEstadoPedido.Estornado:
						return new Response<Pedido?>(null, 400, "Pedido Estornado");
					default:
						return new Response<Pedido?>(null, 400, "Não foi possível fazer pagamento");
				}

				pedido.Estado = EEstadoPedido.Pago;
				pedido.ReferenciaExterna = request.IdPagamentoExterno;
				pedido.DataAlteracao = DateTime.Now;
				context.pedidos.Update(pedido);
				await context.SaveChangesAsync();

				return new Response<Pedido?>(pedido, 200, $"Pedido {pedido.Numero} pago com sucesso");

			}
			catch (Exception)
			{

				return new Response<Pedido?>(null, 500, "Falha o buscar pedido");
			}
		}
	}
}
