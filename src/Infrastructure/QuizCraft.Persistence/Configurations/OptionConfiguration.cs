// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Persistence.Models;

namespace QuizCraft.Persistence.Configurations;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Options_Id");

        builder.Property(e => e.Text).HasMaxLength(500);

        builder.HasOne(d => d.MultipleOptionQuestion).WithMany(p => p.Options)
            .HasForeignKey(d => d.MultipleOptionQuestionId)
            .HasConstraintName("FK_Options_MultipleOptionQuestionId_MultipleOptionQuestions_Id");
    }
}
