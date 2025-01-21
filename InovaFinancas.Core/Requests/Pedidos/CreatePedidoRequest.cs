namespace InovaFinancas.Core.Requests.Pedidos
{
	public class CreatePedidoRequest: Request
	{
        public long ProdutoId { get; set; }
        public long? VoucherId { get; set; }
    }
}
