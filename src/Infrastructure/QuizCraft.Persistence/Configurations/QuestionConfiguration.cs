// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Persistence.Models;

namespace QuizCraft.Persistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Questions_Id");

        builder.Property(e => e.CorrectAnswer).HasMaxLength(400);
        builder.Property(e => e.CreatedAt).HasColumnType("datetime");
        builder.Property(e => e.Text).HasMaxLength(500);
        builder.Property(e => e.UpdatedAt).HasColumnType("datetime");

        builder.HasOne(d => d.Quiz).WithMany(p => p.Questions)
            .HasForeignKey(d => d.QuizId)
            .HasConstraintName("FK_Questions_QuizId_Quizzes_Id");
    }
}
