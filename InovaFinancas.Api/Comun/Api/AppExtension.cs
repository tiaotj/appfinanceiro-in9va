using InovaFinancas.Api.EndPoints;
using InovaFinancas.Api.Models;
using InovaFinancas.Core.Models.Conta;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace InovaFinancas.Api.Comun.Api
{
	public static class AppExtension
	{
		public static void ConfigureDev(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			app.MapSwagger().RequireAuthorization();
		}
		public static void UseSecutity(this WebApplication app)
		{
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapGroup("v1/identity")
				.WithTags("Identity")
				.MapIdentityApi<User>();

			app.MapGroup("v1/identity")
				.WithTags("Identity")
				.MapPost("/logout", async (
					SignInManager<User> singInManager) =>
				{
					await singInManager.SignOutAsync();
					return Results.Ok();
				}).RequireAuthorization();

			app.MapGroup("v1/identity")
				.WithTags("Identity")
				.MapGet("/roles", (
					ClaimsPrincipal user) =>
				{
					if (user.Identity is null || !user.Identity.IsAuthenticated)
						return Results.Unauthorized();
					var ideneity = (ClaimsIdentity)user.Identity;
					var roles = ideneity.FindAll(ideneity.RoleClaimType)
							.Select(x => new RoleClaim
							{
								Issuer =x.Issuer,
								OriginalIssuer = x.OriginalIssuer,
								Type = x.Type,
								Value = x.Value,
								ValueType = x.ValueType
							});
					return TypedResults.Json(roles);
				}).RequireAuthorization();


		}
	}
}
