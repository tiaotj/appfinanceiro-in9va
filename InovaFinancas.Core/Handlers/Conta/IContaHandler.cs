using InovaFinancas.Core.Requests.Conta;
using InovaFinancas.Core.Responses;

namespace InovaFinancas.Core.Handlers.Conta
{
	public interface IContaHandler
	{
		Task<Response<string>> LoginAsync(LoginRequest request);
		Task<Response<string>> RegistroAsync(RegistroRequest request);
		Task LogoutAsync();
	}
}
