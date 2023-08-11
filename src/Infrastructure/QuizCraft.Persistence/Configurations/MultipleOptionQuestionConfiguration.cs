// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Persistence.Models;

namespace QuizCraft.Persistence.Configurations;

public class MultipleOptionQuestionConfiguration : IEntityTypeConfiguration<MultipleOptionQuestion>
{
    public void Configure(EntityTypeBuilder<MultipleOptionQuestion> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_MultipleOptionQuestions_Id");

        builder.HasOne(d => d.Question).WithMany(p => p.MultipleOptionQuestions)
            .HasForeignKey(d => d.QuestionId)
            .HasConstraintName("FK_MultipleOptionQuestions_QuestionId_Questions_Id");
    }
}
