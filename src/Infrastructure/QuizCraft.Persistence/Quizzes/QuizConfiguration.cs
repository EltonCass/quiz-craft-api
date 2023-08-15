// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Quizzes;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PK_Quizzes_Id");

        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired(false);
        builder.Property(e => e.Description)
            .HasMaxLength(400)
            .IsRequired();
        builder.Property(e => e.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.HasOne(d => d.CreatedByUser)
            .WithMany(p => p.QuizzesCreatedBy)
            .HasForeignKey(d => d.CreatedByUserId)
            .HasConstraintName("FK_Quizzes_CreatedByUserId_Users_Id")
            .IsRequired(false);

        builder.HasOne(d => d.UpdatedByUser)
            .WithMany(p => p.QuizzesUpdatedBy)
            .HasForeignKey(d => d.UpdatedByUserId)
            .HasConstraintName("FK_Quizzes_UpdatedByUserId_Users_Id")
            .IsRequired(false);
    }
}
