namespace InovaFinancas.Core.Models.Reports
{
	public record FinancasSummary(string UsuarioId, decimal Entradas, decimal Saidas)
	{
		public decimal Total => Entradas - (Saidas < 0 ? -Saidas: Saidas);
	}
}
