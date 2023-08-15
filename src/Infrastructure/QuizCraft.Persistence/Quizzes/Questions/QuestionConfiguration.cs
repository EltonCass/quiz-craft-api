// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Quizzes.Questions;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Questions_Id");

        builder.Property(e => e.CorrectAnswer)
            .HasMaxLength(400)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired(false);
        builder.Property(e => e.Text)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime")
            .IsRequired(false);
        builder.HasOne(d => d.Quiz)
            .WithMany(p => p.Questions)
            .HasForeignKey(d => d.QuizId)
            .HasConstraintName("FK_Questions_QuizId_Quizzes_Id")
            .IsRequired();
    }
}
