using System.ComponentModel.DataAnnotations;

namespace InovaFinancas.Core.Requests.Conta
{
	public class RegistroRequest:Request
	{
        [Required(ErrorMessage ="E-mail")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Senha Inválida")]
        public string Password { get; set; }
    }
}
