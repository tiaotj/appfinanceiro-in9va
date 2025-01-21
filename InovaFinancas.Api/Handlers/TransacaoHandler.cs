using InovaFinancas.Api.Data;
using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Transacao;
using InovaFinancas.Core.Responses;
using Microsoft.EntityFrameworkCore;
using InovaFinancas.Core.Comun;
using InovaFinancas.Core.Enums;


namespace InovaFinancas.Api.Handlers
{
	public class TransacaoHandler(AppDbConext context) : ITransacaoHandler
	{
		public async Task<Response<Transacao?>> CreateAsync(CreateTransacaoRequest request)
		{
			if (request.Tipo == ETransacaoTipo.Saida && request.Valor > 0)
				request.Valor *= -1;
			try
			{
				var transacao = new Transacao()
				{
					UsuarioId = request.UsuarioId,
					CategoriaId = request.CategoriaId,
					DataCriacao = DateTime.Now,
					Valor = request.Valor,
					DataRecebimento = request.DataRecebimento,
					Tipo = request.Tipo,
					Titulo = request.Titulo,
				};
				await context.AddAsync(transacao);
				await context.SaveChangesAsync();

				return new Response<Transacao?>(transacao, 201,"Transação criada com sucesso!");
			}
			catch {
				return new Response<Transacao?>(null, 500, "Não foi possível criar uma trasação");
			}
		}

		public async Task<Response<Transacao?>> DeleteAsync(DeleteTransacaoRequest request)
		{
			try
			{

				var transacao = await context.Transacoes.FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

				if (transacao == null)
					return new Response<Transacao?>(null, 404, "Transação não existe no banco de dados");

			
				context.Transacoes.Remove(transacao);
				await context.SaveChangesAsync();

				return new Response<Transacao?>(transacao, 200, "Transação excluida com sucesso!");
			}
			catch
			{
				return new Response<Transacao?>(null, 500, "Não foi possível excluir a trasação");
			}
		}

		public async Task<Response<Transacao?>> GetByIdAsync(GetTransacaoByIdRequest request)
		{
			try
			{

				var transacao = await context.Transacoes.FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

				if (transacao == null)
					return new Response<Transacao?>(null, 404, "Transação não existe no banco de dados");

				return new Response<Transacao?>(transacao, 200, "Transação Atualizada com sucesso!");
			}
			catch
			{
				return new Response<Transacao?>(null, 500, "Não foi possível atualizar a trasação");
			}
		}

		public async Task<PageResponse<List<Transacao>?>> GetByPeriodoAsync(GetTransacaoByPeriodoRequest request)
		{
			try
			{


				request.StartDate ??= DateTime.Now.GetFirstDay();
				request.EndDate ??= DateTime.Now.GetLastDay();

				var query = context.Transacoes
							.AsNoTracking()
							.Where(x => x.DataRecebimento >= request.StartDate && x.DataRecebimento <= request.EndDate && x.UsuarioId == request.UsuarioId).OrderBy(x=>x.DataCriacao);

				var count = await query.CountAsync();

				var transacoes = await  query.Skip((request.NumeroPagina-1) * request.TamanhoPagina).Take(request.TamanhoPagina).ToListAsync();

				return new PageResponse<List<Transacao> ?> (transacoes, count, request.NumeroPagina, request.TamanhoPagina);
			}
			catch
			{
				return new PageResponse<List<Transacao>?>(null, 500, "Não foi possível obter as trasações");
			}
		}

		public async Task<Response<Transacao?>> UpdateAsync(UpdateTransacaoRequest request)
		{
			if (request.Tipo == ETransacaoTipo.Saida && request.Valor > 0)
				request.Valor *= -1;
			try
			{

				var transacao = await context.Transacoes.FirstOrDefaultAsync(x=>x.Id == request.Id && x.UsuarioId == request.UsuarioId);

				if (transacao == null)
					return new Response<Transacao?>(null, 404, "Transação não existe no banco de dados");

				transacao.CategoriaId = request.CategoriaId;
				transacao.DataCriacao = DateTime.Now;
				transacao. Valor = request.Valor;
				transacao.DataRecebimento = request.DataRecebimento;
				transacao.Tipo = request.Tipo;
				transacao.Titulo = request.Titulo;
				
				context.Update(transacao);
				await context.SaveChangesAsync();

				return new Response<Transacao?>(transacao, 200, "Transação Atualizada com sucesso!");
			}
			catch
			{
				return new Response<Transacao?>(null, 500, "Não foi possível atualizar a trasação");
			}
		}
	}
}
