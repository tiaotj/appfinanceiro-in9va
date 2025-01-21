namespace InovaFinancas.Core.Models
{
	public class Produto
	{
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = "";
        public string Slug { get; set; } = "";
        public bool IsAtivo { get; set; } = true;
        public decimal Valor { get; set; }
    }
}
