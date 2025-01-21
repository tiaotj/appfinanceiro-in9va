using System.Text.Json.Serialization;

namespace InovaFinancas.Core.Responses
{
	public class PageResponse<TData>:Response<TData>
	{
        [JsonConstructor]
        public PageResponse(
            TData? data, 
            int totalRegistros, 
            int paginaAtual = 1, 
            int tamanhoPagina = Configuration.DefaultTamanhoPagina):base(data)
        {
            Data = data;
            TotalRegistros = totalRegistros;
            PaginaAtual = paginaAtual;
            TamanhoPagina = tamanhoPagina;
        }
        public PageResponse(TData? data, int code = Configuration.DefaultCode,
            string? mensagem = null):base(data,code,mensagem)
        {
            
        }
        public int PaginaAtual { get; set; }
        public int TotalPaginas => (int)Math.Ceiling(TotalRegistros / (double)TamanhoPagina);
        public int TamanhoPagina { get; set; } = Configuration.DefaultTamanhoPagina;
        public int TotalRegistros { get; set; }

    }
}
