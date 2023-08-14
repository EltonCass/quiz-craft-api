// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Quizzes.Questions;

public class FillInBlankQuestionConfiguration
    : IEntityTypeConfiguration<FillInBlankQuestion>
{
    public void Configure(EntityTypeBuilder<FillInBlankQuestion> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PK_FillInBlankQuestions_Id");

        builder.HasOne(d => d.Question)
            .WithOne(p => p.FillInBlankQuestion)
            .HasForeignKey<Question>(d => d.Id)
            .HasConstraintName("FK_FillInBlankQuestions_QuestionId_Questions_Id");
    }
}
