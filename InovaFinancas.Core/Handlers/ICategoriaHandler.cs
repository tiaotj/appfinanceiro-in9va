
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Requests.Categorias;
using InovaFinancas.Core.Responses;

namespace InovaFinancas.Core.Handlers
{
	public interface ICategoriaHandler
	{
		Task<Response<Categoria?>> CreateAsync(CreateCategoriaRequest request);
		Task<Response<Categoria?>> UpdateAsync(UpdateCategoriaRequest request);
		Task<Response<Categoria?>> DeleteAsync(DeleteCategoriaRequest request);
		Task<PageResponse<List<Categoria>>> GetAllAsync(GetAllCategoriaRequest request);
		Task<Response<Categoria?>> GetByIdAsync(GetCategoriaByIdRequest request);
	}
}
