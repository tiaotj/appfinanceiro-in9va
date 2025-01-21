namespace InovaFinancas.Core.Requests.Pedidos
{
	public class GetPedidoByNumeroRequest:Request
	{
		public string Numero { get; set; } = "";
    }
}
