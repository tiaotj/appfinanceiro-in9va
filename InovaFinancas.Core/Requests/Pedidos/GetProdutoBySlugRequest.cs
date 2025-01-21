namespace InovaFinancas.Core.Requests.Pedidos
{
	public class GetProdutoBySlugRequest:Request
	{
		public string Slug { get; set; } = "";
    }
}
