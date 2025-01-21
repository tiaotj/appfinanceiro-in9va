using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;

namespace InovaFinancas.Core.Handlers
{
	public interface IPedidoHandler
	{
		Task<Response<Pedido?>> CancelarAsync(CancelarPedidoRequest request);
		Task<Response<Pedido?>> CreateAsync(CreatePedidoRequest request);
		Task<Response<Pedido?>> PagarAsync(PagamentoPedidoRequest request);
		Task<Response<Pedido?>> EstornarAsync(EstornoPedidoRequest request);
		Task<PageResponse<List<Pedido>?>> GetAllAsync(GetAllPedidosRequest request);
		Task<Response<Pedido?>> GetByNumeroAsync(GetPedidoByNumeroRequest request);
	}
}
