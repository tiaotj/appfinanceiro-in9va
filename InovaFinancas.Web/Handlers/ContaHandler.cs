using InovaFinancas.Core.Handlers.Conta;
using InovaFinancas.Core.Requests.Conta;
using InovaFinancas.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace InovaFinancas.Web.Handlers
{
	public class ContaHandler(IHttpClientFactory httpClientFactory) : IContaHandler
	{
		private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
		public async Task<Response<string>> LoginAsync(LoginRequest request)
		{
			var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
			return result.IsSuccessStatusCode ?
				new Response<string>("Login realizado com sucesso!", 200, "Login OK")
				: new Response<string>("", 400, "Não foi possível realizar login");
		}

		public async Task LogoutAsync()
		{
			var emptyContent = new StringContent("{}", Encoding.UTF8,"application/json");
			await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
		}

		public async Task<Response<string>> RegistroAsync(RegistroRequest request)
		{
			//_client.BaseAddress = new Uri("http://localhost:5144");
			var result = await _client.PostAsJsonAsync("v1/identity/register", request);
			return result.IsSuccessStatusCode ?
				new Response<string>("Cadastro realizado com sucesso!", 200, "Registro OK")
				: new Response<string>("", 400, "Não foi possível realizar registro");
		}
	}
}
