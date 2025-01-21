using InovaFinancas.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InovaFinancas.Api.Data.Mappings
{
	public class TransacaoMapping:IEntityTypeConfiguration<Transacao>
	{
		public void Configure(EntityTypeBuilder<Transacao> builder)
		{
			builder.ToTable("Transacao");
			builder.HasKey(t => t.Id)				;
			builder.Property(x => x.Titulo)
				.IsRequired(true)
				.HasColumnType("NVARCHAR")
				.HasMaxLength(80);
			builder.Property(x => x.Tipo)
				.IsRequired(true)
				.HasColumnType("SMALLINT");
			builder.Property(x => x.Valor)
				.HasColumnType("MONEY");
			builder.Property(x => x.DataCriacao)
				.IsRequired();
			builder.Property(x => x.DataRecebimento)
				.IsRequired(false);
			builder.Property(x => x.UsuarioId)
				.IsRequired()
				.HasColumnType("VARCHAR")
				.HasMaxLength(160);
		}
	}
}
