// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Persistence.Models;

namespace QuizCraft.Persistence.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Quizzes_Id");

        builder.Property(e => e.CreatedAt).HasColumnType("datetime");
        builder.Property(e => e.Description).HasMaxLength(400);
        builder.Property(e => e.Title).HasMaxLength(200);
        builder.Property(e => e.UpdatedAt).HasColumnType("datetime");

        builder.HasOne(d => d.CreatedByNavigation).WithMany(p => p.QuizCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .HasConstraintName("FK_Quizzes_CreatedBy_Users_Id");

        builder.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.QuizUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .HasConstraintName("FK_Quizzes_UpdatedBy_Users_Id");
    }
}
