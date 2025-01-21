using MudBlazor;

namespace InovaFinancas.Web
{
	public static class Configuration
	{
		public const string HttpClientName = "inova";
		public static string BackEndURL = "http://localhost:5144";
		public static string StripePublicKey = "";

		public static MudTheme Theme = new()
		{
			Typography = new()
			{
				Default = new Default()
				{
					FontFamily = ["Merienda", "cursive"]
				}
			},
			PaletteLight = new PaletteLight
			{
				Primary = "#8c51e8",
				Secondary = "#f15f09",
				Background = Colors.Gray.Lighten4,
				AppbarBackground = "#8c51e8"
			},
			PaletteDark = new PaletteDark
			{
				AppbarBackground = "#8c51e8"
			}

		};
	}
}
