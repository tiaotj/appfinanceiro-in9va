using InovaFinancas.Core.Enums;

namespace InovaFinancas.Core.Models
{
	public  class Pedido
	{
        public long Id { get; set; }
        public long ProdutoId { get; set; }
        public string Numero { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
        public string? ReferenciaExterna { get; set; }
		public EGatewayPagamento Gateway { get; set; }
		public EEstadoPedido Estado { get; set; }

        public Produto Produto { get; set; } = null!;
        public long? VoucherId { get; set; }
        public Voucher? Voucher { get; set; } = null!;
        public string UserId { get; set; } = "";
        public decimal Total =>Produto.Valor - (Voucher?.Valor ?? 0);

    }
}
