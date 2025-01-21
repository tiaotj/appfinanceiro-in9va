namespace InovaFinancas.Core.Models
{
	public class Categoria
	{
        public long Id { get; set; }
        public string Titulo { get; set; }= string.Empty;
        public string? Descricao { get; set; }=string.Empty;
        public string UsuarioId { get; set; } = string.Empty;
    }
}
