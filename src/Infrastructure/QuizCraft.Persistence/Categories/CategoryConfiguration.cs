// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id)
            .HasName("PK_Categories_Id");
        builder.HasIndex(c => c.Name, "UQ_Categories_Name")
            .IsUnique();

        builder.Property(e => e.Description)
            .HasMaxLength(100)
            .IsRequired(false);
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        
    }
}
