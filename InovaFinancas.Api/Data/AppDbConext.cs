using InovaFinancas.Api.Models;
using InovaFinancas.Core.Models;
using InovaFinancas.Core.Models.Reports;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Principal;

namespace InovaFinancas.Api.Data
{
	public class AppDbConext:IdentityDbContext
		<User,
		IdentityRole<long>, 
		long,
		IdentityUserClaim<long>,
		IdentityUserRole<long>,
		IdentityUserLogin<long>,
		IdentityRoleClaim<long>,
		IdentityUserToken<long>>
	{
        public AppDbConext(DbContextOptions<AppDbConext> options):base(options)
        {
				
        }
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Transacao> Transacoes { get; set; } = null!;

        public DbSet<EntradasSaidas> EntrasSaidas { get; set; } = null!;
        public DbSet<EntradasByCategoria> EntradasByCategoria { get; set; } = null!;
        public DbSet<SaidasByCategoria> SaidasByCategoria { get; set; } = null!;

        public DbSet<Voucher> Voucheres { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			modelBuilder.Entity<EntradasSaidas>().HasNoKey().ToView("vwGetEntradasSaidas");
			modelBuilder.Entity<EntradasByCategoria>().HasNoKey().ToView("vwGetEntradasByCategoria");
			modelBuilder.Entity<SaidasByCategoria>().HasNoKey().ToView("vwSaidasByCategoria");
		}
	}
}
