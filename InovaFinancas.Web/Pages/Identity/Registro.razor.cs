using InovaFinancas.Core.Handlers.Conta;
using InovaFinancas.Core.Requests.Conta;
using InovaFinancas.Web.Seguranca;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace InovaFinancas.Web.Pages.Identity
{
	public partial class RegistroPage : ComponentBase
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

		#region Propriedades
		public bool IsBusy { get; set; } = false;
        public RegistroRequest InputModel { get; set; } = new();
		#endregion

		#region Overrides
		protected override async Task OnInitializedAsync()
		{
			var authState = await AuthenticationProvider.GetAuthenticationStateAsync();
			var user = authState.User;
			if (user.Identity is not null && user.Identity.IsAuthenticated)
				Navigation.NavigateTo("/");
		}
		#endregion

		#region Metodos
		public async Task OnValidSubmitAsync()
		{
			IsBusy = true;
			try
			{
				var result = await Handler.RegistroAsync(InputModel);
				if (result.IsSucesso)
				{
					Snackbar.Add(result.Mensagem, Severity.Success);
					Navigation.NavigateTo("/login");
				}					
				else
					Snackbar.Add(result.Mensagem,Severity.Error);
               
            }
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
				throw;
			}
			finally
			{
				IsBusy = false;
			}
		}
		#endregion

	}
}
