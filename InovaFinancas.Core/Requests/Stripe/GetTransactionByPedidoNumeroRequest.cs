namespace InovaFinancas.Core.Requests.Stripe
{
	public class GetTransactionByPedidoNumeroRequest:Request
	{
		public string Numero { get; set; } = "";
    }
}
