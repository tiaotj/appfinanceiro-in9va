namespace InovaFinancas.Core.Models.Conta
{
	public class Usuario
	{
		public string Email { get; set; } = "";
		public Dictionary<string, string> Claims { get; set; } = [];
    }
}
