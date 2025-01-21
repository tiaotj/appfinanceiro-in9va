namespace InovaFinancas.Core.Requests.Transacao
{
	public class GetTransacaoByPeriodoRequest:PageRequest
	{
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
