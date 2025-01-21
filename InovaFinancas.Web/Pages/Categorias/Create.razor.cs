using InovaFinancas.Core.Handlers;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Web.Handlers.Categorias;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;


namespace InovaFinancas.Web.Pages.Categorias
{
	public partial class CreateCategoriaPage: ComponentBase
	{
        #region Propriedades
        public bool IsBusy { get; set; } = false;
        public CreateCategoriaRequest inputModel { get; set; } = new();
        #endregion

        #region Servicos
        [Inject]
        public ICategoriaHandler Handler { get; set; } = null!;
		[Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
		[Inject]
        public ISnackbar Snackbar { get; set; } = null!;

		#endregion

		#region Metodos
		public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var result = await Handler.CreateAsync(inputModel);
                if (result.IsSucesso)
                {
                    Snackbar.Add(result.Mensagem, Severity.Success);
					NavigationManager.NavigateTo("/categorias");
                }
                else
					Snackbar.Add(result.Mensagem, Severity.Error);

			}
            catch (Exception ex)
            {
				Snackbar.Add(ex.Message, Severity.Error);
			}
            finally
            {
				IsBusy = false;
			}

            
        }
        #endregion
    }
}
