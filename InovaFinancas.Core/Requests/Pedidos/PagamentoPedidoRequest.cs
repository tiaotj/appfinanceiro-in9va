namespace InovaFinancas.Core.Requests.Pedidos
{
	public class PagamentoPedidoRequest:Request
	{
        public string Numero { get; set; } = "";
        public string IdPagamentoExterno { get; set; } = "";
    }
}
