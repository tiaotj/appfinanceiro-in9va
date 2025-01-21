using InovaFinancas.Core.Handlers.Conta;
using InovaFinancas.Web.Seguranca;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Identity
{
	public partial class LogoutPage: ComponentBase
	{
		#region dependencias
		[Inject]
		public ISnackbar Snackbar { get; set; } = null!;
		[Inject]
		public IContaHandler Handler { get; set; } = null!;
		[Inject]
		public NavigationManager Navigation { get; set; } = null!;
		[Inject]
		public ICookieAuthenticationStateProvider AuthenticationProvider { get; set; } = null!;
		#endregion

		#region Overrides
		protected override async Task OnInitializedAsync()
		{
			if(await AuthenticationProvider.CheckAuthenticatedAsync())
			{
				await Handler.LogoutAsync();
				await AuthenticationProvider.GetAuthenticationStateAsync();
				AuthenticationProvider.NotifyAuthenticationStateChanged();
			}
			await base.OnInitializedAsync();
				
		}
		#endregion
	}
}
