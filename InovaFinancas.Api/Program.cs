using InovaFinancas.Api;
using InovaFinancas.Api.Comun.Api;
using InovaFinancas.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddSwagger();
builder.AddServices();



var app = builder.Build();

//if (app.Environment.IsDevelopment())
	app.ConfigureDev();


app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecutity();

app.MapEndPoints();

app.Run();

#region UsodoMap

//Endpoints
//app.MapPost("/v1/Categoria", async (CreateCategoriaRequest request, ICategoriaHandler handler)
//			=> await handler.CreateAsync(request))
//			.WithName("Criar: Categoria")
//			.WithSummary("Criar uma nova categoria")
//			.Produces<Response<Categoria>>();

//app.MapPut("/v1/Categoria/{id}", async (long id, UpdateCategoriaRequest request, ICategoriaHandler handler)
//			=>
//			{
//				request.Id = id;
//				return await handler.UpdateAsync(request);
//			})
//			.WithName("Atualizar: Categoria")
//			.WithSummary("Atualizar uma nova categoria")
//			.Produces<Response<Categoria>>();

//app.MapDelete("/v1/Categoria/{id}", async (long id, ICategoriaHandler handler)
//			=>
//			{
//				var request = new DeleteCategoriaRequest()
//				{
//					Id = id
//				};

//				return await handler.DeleteAsync(request);
//			})
//			.WithName("Deletar: Categoria")
//			.WithSummary("Delata uma categoria")
//			.Produces<Response<Categoria>>();

//app.MapGet("/v1/Categoria/{id}", async (long id, ICategoriaHandler handler)
//			=>
//			{
//				var request = new GetCategoriaByIdRequest()
//				{
//					Id = id,
//					UsuarioId = "Tiao"
//				};
//			return await handler.GetByIdAsync(request);
//			})
//			.WithName("Retornar: Categoria")
//			.WithSummary("Retorna uma categoria")
//			.Produces<Response<Categoria>>();

//app.MapGet("/v1/Categoria", async (ICategoriaHandler handler)
//			=>
//{
//	var request = new GetAllCategoriaRequest()
//	{
//		UsuarioId = "Tiao"
//	};
//	return await handler.GetAllAsync(request);
//})
//			.WithName("obter: Categoria")
//			.WithSummary("Obter todas as categoria")
//			.Produces<PageResponse<List<Categoria>?>>();
#endregion

