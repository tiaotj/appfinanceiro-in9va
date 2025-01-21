namespace InovaFinancas.Core.Requests.Pedidos
{
	public class GetVoucherByNumeroRequest:Request
	{
		public string Numero { get; set; } = "";
    }
}
