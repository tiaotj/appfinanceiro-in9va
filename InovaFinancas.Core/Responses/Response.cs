using System.Text.Json.Serialization;

namespace InovaFinancas.Core.Responses
{
	public class Response<TData>
	{

		private readonly int _code;

		[JsonConstructor]
        public Response()
        {
			_code = Configuration.DefaultCode; 
        }
        public Response(TData? data, int code= Configuration.DefaultCode, string? mensagem=null)
		{
			_code = code;
			Data = data;
			Mensagem = mensagem;

		}

		public TData? Data { get; set; }
		public string Mensagem { get; set; } = "";
		[JsonIgnore]
		public bool IsSucesso => _code is >= 200 and <= 299;
	}
}
