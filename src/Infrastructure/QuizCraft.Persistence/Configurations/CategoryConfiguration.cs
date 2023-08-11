// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Persistence.Models;

namespace QuizCraft.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Categories_Id");

        builder.HasIndex(e => e.Name, "UQ_Categories_Name").IsUnique();

        builder.Property(e => e.Description).HasMaxLength(100);
        builder.Property(e => e.Name)
        .HasMaxLength(100)
        .IsUnicode(false);
    }
}
