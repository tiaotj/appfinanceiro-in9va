namespace InovaFinancas.Core.Requests
{
	public abstract class PageRequest:Request
	{
        public int NumeroPagina { get; set; } = Configuration.NumeroPagina;
        public int TamanhoPagina { get; set; } = Configuration.DefaultTamanhoPagina;
    }
}
