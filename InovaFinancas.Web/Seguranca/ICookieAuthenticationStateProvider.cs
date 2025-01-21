using Microsoft.AspNetCore.Components.Authorization;

namespace InovaFinancas.Web.Seguranca
{
	public interface ICookieAuthenticationStateProvider
	{
		Task<bool> CheckAuthenticatedAsync();
		Task<AuthenticationState> GetAuthenticationStateAsync();
		void NotifyAuthenticationStateChanged();
	}
}
