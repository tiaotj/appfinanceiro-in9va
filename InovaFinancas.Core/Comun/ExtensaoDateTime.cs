namespace InovaFinancas.Core.Comun
{
	public static class ExtensaoDateTime
	{
		public static DateTime GetFirstDay(this DateTime date, int? year = null, int? month = null)
		{
			return new DateTime(year ?? date.Year, month ?? date.Month, 1);

		}

		public static DateTime GetLastDay(this DateTime date, int? year = null, int? month = null)
		{
		
			var d = new DateTime(year ?? date.Year, month ?? date.Month, 1).AddMonths(1).AddDays(-1);

			return d;
		}
	}
}
