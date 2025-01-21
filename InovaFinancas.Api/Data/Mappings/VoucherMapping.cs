using InovaFinancas.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InovaFinancas.Api.Data.Mappings
{
	public class VoucherMapping:IEntityTypeConfiguration<Voucher>
	{
		public void Configure(EntityTypeBuilder<Voucher> builder)
		{
			builder.ToTable("Voucher");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Numero)
				.IsRequired(true)
				.HasColumnType("CHAR")
				.HasMaxLength(8);

			builder.Property(x => x.Titulo)
				.IsRequired(true)
				.HasColumnType("NVARCHAR")
				.HasMaxLength(80);

			builder.Property(x => x.Descricao)
				.IsRequired(false)
				.HasColumnType("NVARCHAR")
				.HasMaxLength(255);

			builder.Property(x => x.Valor)
				.IsRequired(true)
				.HasColumnType("MONEY");

			builder.Property(x => x.IsAtivo)
				.IsRequired(true)
				.HasColumnType("BIT");
		}
	}
}
