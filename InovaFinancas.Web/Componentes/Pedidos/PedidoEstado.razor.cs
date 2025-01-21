using InovaFinancas.Core.Enums;
using Microsoft.AspNetCore.Components;

namespace InovaFinancas.Web.Componentes.Pedidos
{
	public partial class PedidoEstadoComponet: ComponentBase
	{
		[Parameter, EditorRequired]
		public EEstadoPedido EstadoPedido		{ get; set; }
    }
}
