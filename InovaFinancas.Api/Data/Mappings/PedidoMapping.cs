using InovaFinancas.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InovaFinancas.Api.Data.Mappings
{
	public class PedidoMapping: IEntityTypeConfiguration<Pedido>
	{
		public void Configure(EntityTypeBuilder<Pedido> builder)
		{
			builder.ToTable("Pedido");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Numero)
				.IsRequired(true)
				.HasColumnType("CHAR")
				.HasMaxLength(8);

			builder.Property(x => x.ReferenciaExterna)
				.IsRequired(false)
				.HasColumnType("VARCHAR")
				.HasMaxLength(60);

			builder.Property(x => x.Gateway)
				.IsRequired(true)
				.HasColumnType("SMALLINT");

			builder.Property(x => x.DataCadastro)
				.IsRequired(true)
				.HasColumnType("DATETIME2");

			builder.Property(x => x.DataAlteracao)
				.IsRequired(true)
				.HasColumnType("DATETIME2");

			builder.Property(x => x.Estado)
				.IsRequired(true)
				.HasColumnType("SMALLINT");

			builder.Property(x => x.UserId)
				.IsRequired(true)
				.HasColumnType("VARCHAR")
				.HasMaxLength(160);

			builder.HasOne(x => x.Produto).WithMany();
			builder.HasOne(x => x.Voucher).WithMany();
		}
	}
}
