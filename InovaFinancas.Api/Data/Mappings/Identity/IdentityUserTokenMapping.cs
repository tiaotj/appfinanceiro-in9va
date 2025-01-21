﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace InovaFinancas.Api.Data.Mappings.Identity
{
    public class IdentityUserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
        {
            builder.ToTable("IdentityUserToken");
            builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            builder.Property(x => x.LoginProvider).HasMaxLength(120);
            builder.Property(x => x.Name).HasMaxLength(180);

        }
    }
}
