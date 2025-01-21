using InovaFinancas.Api.Data;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace InovaFinancas.Api.Handlers
{
	public class CategoriaHandler(AppDbConext context) : ICategoriaHandler
	{

		public async Task<Response<Categoria?>> CreateAsync(CreateCategoriaRequest request)
		{
			try
			{
				var categoria = new Categoria()
				{
					UsuarioId = request.UsuarioId,
					Titulo = request.Titulo,
					Descricao = request.Descricao,
				};
				await context.AddAsync(categoria);
				await context.SaveChangesAsync();

				return new Response<Categoria?>(categoria, 201, "Categoria criada com sucesso!!!");
			}
			catch
			{
				return new Response<Categoria?>(null, 500, "Não foi possível criar uma nova Categoria");
			}
		}

		public async Task<Response<Categoria?>> UpdateAsync(UpdateCategoriaRequest request)
		{
			try
			{

				var categoriaBanco = await context.Categorias
							.FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

				if (categoriaBanco == null)
					return new Response<Categoria?>(null, 404, "Categoria não encontrada");

				categoriaBanco.UsuarioId = request.UsuarioId;
				categoriaBanco.Titulo = request.Titulo;
				categoriaBanco.Descricao = request.Descricao;

				context.Update(categoriaBanco);
				await context.SaveChangesAsync();

				return new Response<Categoria?>(categoriaBanco, mensagem: "Categoria atualizada com sucesso!!!");
			}
			catch
			{
				return new Response<Categoria?>(null, 500, "[FP079]Não foi possível atualizar a Categoria");
			}
		}

		public async Task<Response<Categoria?>> DeleteAsync(DeleteCategoriaRequest request)
		{
			try
			{

				var categoriaBanco = await context.Categorias
							.FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

				if (categoriaBanco == null)
					return new Response<Categoria?>(null, 404, "Categoria não encontrada");

				context.Remove(categoriaBanco);
				await context.SaveChangesAsync();

				return new Response<Categoria?>(categoriaBanco, mensagem: "Categoria excluida com sucesso!!!");
			}
			catch (Exception e)
			{
				return new Response<Categoria?>(null, 500, "[FP079]Não foi possível excluir a Categoria");
			}
		}

		public async Task<PageResponse<List<Categoria>>> GetAllAsync(GetAllCategoriaRequest request)
		{
			try
			{

				var queri = context.Categorias
							.AsNoTracking()
							.Where(x => x.UsuarioId == request.UsuarioId).OrderBy(x=>x.Titulo);

				var categorias = await queri
							.Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
							.Take(request.TamanhoPagina)
							.ToListAsync();


				var count = await queri.CountAsync();

				return new PageResponse<List<Categoria>>(categorias, count, request.NumeroPagina, request.TamanhoPagina);

			}
			catch
			{
				return new PageResponse<List<Categoria>>(null, 500, "[FP079]Não foi trazer as categorias");
			}
		}

		public async Task<Response<Categoria?>> GetByIdAsync(GetCategoriaByIdRequest request)
		{
			try
			{

				var categoriaBanco = await context.Categorias
							.AsNoTracking()
							.FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

				if (categoriaBanco == null)
					return new Response<Categoria?>(null, 404, "Categoria não encontrada");


				return new Response<Categoria?>(categoriaBanco);
			}
			catch
			{
				return new Response<Categoria?>(null, 500, "[FP079]Não foi possível atualizar a Categoria");
			}
		}


	}
}
