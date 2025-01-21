using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Transacao;
using InovaFinancas.Core.Responses;

namespace InovaFinancas.Core.Handlers
{
	public interface ITransacaoHandler
	{
		Task<Response<Transacao?>> CreateAsync(CreateTransacaoRequest request);
		Task<Response<Transacao?>> UpdateAsync(UpdateTransacaoRequest request);
		Task<Response<Transacao?>> DeleteAsync(DeleteTransacaoRequest request);
		Task<Response<Transacao?>> GetByIdAsync(GetTransacaoByIdRequest request);
		Task<PageResponse<List<Transacao>?>> GetByPeriodoAsync(GetTransacaoByPeriodoRequest request);
	}
}
