namespace InovaFinancas.Core.Models
{
	public class Voucher
	{
        public long Id { get; set; }
        public string Numero { get; set; } = "";
        public string Titulo { get; set; } = "";
		public string Descricao { get; set; } = "";
		public bool IsAtivo { get; set; } = true;
		public decimal Valor { get; set; } 

	}
}
