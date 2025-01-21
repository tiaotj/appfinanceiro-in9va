using InovaFinancas.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InovaFinancas.Api.Data.Mappings
{
	public class ProdutoMapping:IEntityTypeConfiguration<Produto>
	{
		public void Configure(EntityTypeBuilder<Produto> builder)
		{
			builder.ToTable("Produto");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Titulo)
				.IsRequired(true)
				.HasColumnType("NVARCHAR")
				.HasMaxLength(80);

			builder.Property(x => x.Descricao)
				.IsRequired(false)
				.HasColumnType("NVARCHAR")
				.HasMaxLength(255);

			builder.Property(x => x.Slug)
				.IsRequired(false)
				.HasColumnType("VARCHAR")
				.HasMaxLength(80);

			builder.Property(x => x.Valor)
				.IsRequired(true)
				.HasColumnType("MONEY");

			builder.Property(x => x.IsAtivo)
				.IsRequired(true)
				.HasColumnType("BIT");
		}
	}
}
