using InovaFinancas.Core.Models.Conta;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace InovaFinancas.Web.Seguranca
{
	public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory) : AuthenticationStateProvider, ICookieAuthenticationStateProvider
	{
		private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
		private bool _isAuthentication=false;
		public async Task<bool> CheckAuthenticatedAsync()
		{
			await GetAuthenticationStateAsync();
			return _isAuthentication;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			_isAuthentication = false;
			var user = new ClaimsPrincipal(new ClaimsIdentity());

			var userInfo = await GetUser();
			if (userInfo is null)
				return new AuthenticationState(user);

			var calims = await GetClaims(userInfo);

			var id = new ClaimsIdentity(calims, nameof(CookieAuthenticationStateProvider));
			user = new ClaimsPrincipal(id);

			_isAuthentication = true;
			return new AuthenticationState(user);
		}
		private async Task<Usuario> GetUser()
		{
			try
			{
				var ret = await _client.GetFromJsonAsync<Usuario?>("v1/identity/manage/info");
				return ret;
			}
			catch (Exception e)
			{
				var message = e.Message;
				return null;
			}
			
		}
		private async Task<List<Claim>> GetClaims(Usuario user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,user.Email),
				new Claim(ClaimTypes.Email,user.Email),
			};

			claims.AddRange(
				user.Claims.Where(x=>
					x.Key != ClaimTypes.Name && 
					x.Key != ClaimTypes.Email).Select(x=> 
						new Claim(x.Key,x.Value))
					);

			RoleClaim[]? roles;
			try
			{
				roles = await _client.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
			}
			catch 
			{
				return claims;
			}

			foreach (var item in roles ?? [])
			{
				if (!string.IsNullOrEmpty(item.Type) && !string.IsNullOrEmpty(item.Value))
					claims.Add(new Claim(item.Type, item.Value, item.ValueType,item.Issuer,item.OriginalIssuer));
			}

			return claims;
		}
		


		public void NotifyAuthenticationStateChanged()
		{
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}
	}
}
