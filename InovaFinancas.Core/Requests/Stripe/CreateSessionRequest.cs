namespace InovaFinancas.Core.Requests.Stripe
{
	public class CreateSessionRequest:Request
	{
		public string PedidoNumero { get; set; } = string.Empty;
	   public string ProdutoTitulo { get; set; } = "";
        public string ProdutoDescricao { get; set; } = "";
        public long Total { get; set; }
    }
}
