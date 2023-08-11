// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Persistence.Models;

namespace QuizCraft.Persistence.Configurations;

public class CategoriesQuizConfiguration : IEntityTypeConfiguration<CategoriesQuiz>
{
    public void Configure(EntityTypeBuilder<CategoriesQuiz> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_CategoriesQuizzes_Id");

        builder.HasOne(d => d.Category).WithMany(p => p.CategoriesQuizzes)
            .HasForeignKey(d => d.CategoryId)
            .HasConstraintName("FK_CategoriesQuizzes_CategoryId_Categories_Id");

        builder.HasOne(d => d.Quiz).WithMany(p => p.CategoriesQuizzes)
            .HasForeignKey(d => d.QuizId)
            .HasConstraintName("FK_CategoriesQuizzes_QuizId_Quizzes_Id");
    }
}
