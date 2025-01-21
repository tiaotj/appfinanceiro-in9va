using InovaFinancas.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace InovaFinancas.Core.Requests.Transacao
{
	public class UpdateTransacaoRequest: Request
	{
        public long Id { get; set; }
        [Required(ErrorMessage = "Título Obrigatório")]
		public string Titulo { get; set; }
		[Required(ErrorMessage = "Tipo Obrigatório")]
		public ETransacaoTipo Tipo { get; set; }
		[Required(ErrorMessage = "Valor Obrigatório")]
		public decimal Valor { get; set; }
		[Required(ErrorMessage = "Data Obrigatório")]
		public DateTime? DataRecebimento { get; set; }
		[Required(ErrorMessage = "Categoria Obrigatório")]
		public long CategoriaId { get; set; }
	}
}
