using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;

namespace InovaFinancas.Core.Handlers
{
	public interface IProdutoHandler
	{
		Task<PageResponse<List<Produto>?>> GetAllAsync(GetAllProdutosRequest request);
		Task<Response<Produto?>> GetBySlugAsync(GetProdutoBySlugRequest request);
	}
}
