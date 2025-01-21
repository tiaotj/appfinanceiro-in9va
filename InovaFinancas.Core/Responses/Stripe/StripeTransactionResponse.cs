namespace InovaFinancas.Core.Responses.Stripe
{
	public class StripeTransactionResponse
	{
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public long Valor { get; set; }
        public long ValorCapturado { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool Pago { get; set; }
        public bool Estornado { get; set; }
    }
}
