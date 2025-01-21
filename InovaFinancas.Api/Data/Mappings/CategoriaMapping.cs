﻿using InovaFinancas.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InovaFinancas.Api.Data.Mappings
{
	public class CategoriaMapping:IEntityTypeConfiguration<Categoria>
	{
		public void Configure(EntityTypeBuilder<Categoria> builder)
		{
			builder.ToTable("Categoria");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Titulo)
				.IsRequired()
				.HasColumnType("NVARCHAR")
				.HasMaxLength(80);
			builder.Property(x => x.Descricao)
				.IsRequired(false)
				.HasColumnType("NVARCHAR")
				.HasMaxLength(255);
			builder.Property(x => x.UsuarioId)
				.IsRequired()
				.HasColumnType("VARCHAR")
				.HasMaxLength(160);
		}
    }
}
