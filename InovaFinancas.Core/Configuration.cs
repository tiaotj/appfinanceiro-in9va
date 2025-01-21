namespace InovaFinancas.Core
{
	public static class Configuration
	{
		public const int NumeroPagina = 1;
		public  const int DefaultTamanhoPagina = 25;
		public const int DefaultCode = 200;

		public static string ConnectionString { get; set; } = string.Empty;
		public static string FronEndUrl { get; set; } = string.Empty;
		public static string BackEndUrl { get; set; } = string.Empty;


	}
}
