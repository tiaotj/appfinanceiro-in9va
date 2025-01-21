using System.ComponentModel.DataAnnotations;

namespace InovaFinancas.Core.Requests.Categorias
{
	public class CreateCategoriaRequest:Request
	{
        [Required(ErrorMessage ="Título Inválido")]
        [MaxLength(80,ErrorMessage ="Título deve ter tamanho máximo de 80 caracter")]
        public string Titulo { get; set; } = "";
		[Required(ErrorMessage = "Descrição Inválida")]
		public string? Descricao { get; set; }
    }
}
