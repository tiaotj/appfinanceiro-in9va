using InovaFinancas.Api.Data;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Pedidos;
using InovaFinancas.Core.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;

namespace InovaFinancas.Api.Handlers
{
	public class ProdutoHandler(AppDbConext context) : IProdutoHandler
	{
		public async Task<PageResponse<List<Produto>?>> GetAllAsync(GetAllProdutosRequest request)
		{
			try
			{
				var query = context.Produtos.AsNoTracking().Where(x => x.IsAtivo).OrderBy(x => x.Titulo);

				var produtos = await query.Skip(request.NumeroPagina -1).Take(request.TamanhoPagina).ToListAsync();
				var count = query.Count();

				return new PageResponse<List<Produto>?>(produtos,count,request.NumeroPagina,request.TamanhoPagina); 
			}
			catch (Exception)
			{

				return new PageResponse<List<Produto>?>(null, 500, "Não foi possível carregarLista");
			}
		}

		public async Task<Response<Produto?>> GetBySlugAsync(GetProdutoBySlugRequest request)
		{
			try
			{
				var produto = await context.Produtos.AsNoTracking().FirstOrDefaultAsync(x=>x.Slug == request.Slug);

				return produto is null ? 
					new Response<Produto?>(null, 404, "Produto não encontrado") :
					new Response<Produto?>(produto);

			}
			catch (Exception)
			{
				return new Response<Produto?>(null, 500, "Não foi possível pesquisar o produto");
			}
		}
	}
}
