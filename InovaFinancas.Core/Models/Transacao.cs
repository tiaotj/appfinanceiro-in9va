using InovaFinancas.Core.Enums;

namespace InovaFinancas.Core.Models
{
	public class Transacao
	{
        public long Id { get; set; }
        public string    Titulo { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataRecebimento { get;set;}

        public ETransacaoTipo Tipo { get; set; }= ETransacaoTipo.Entrada;
        public decimal Valor { get; set; }
        public long CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public string UsuarioId { get; set; }= string.Empty;
    }
}
