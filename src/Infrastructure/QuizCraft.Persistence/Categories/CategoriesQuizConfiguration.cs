// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Categories;

public class CategoriesQuizConfiguration : IEntityTypeConfiguration<CategoriesQuiz>
{
    public void Configure(EntityTypeBuilder<CategoriesQuiz> builder)
    {
        builder.HasKey(c => c.Id)
            .HasName("PK_CategoriesQuizzes_Id");
        builder.HasOne(c => c.Category)
            .WithMany(c => c.CategoryQuizzes)
            .HasForeignKey(c => c.CategoryId)
            .HasConstraintName("FK_CategoriesQuizzes_CategoryId_Categories_Id");
        builder.HasOne(c => c.Quiz)
            .WithMany(p => p.CategoriesQuizzes)
            .HasForeignKey(d => d.QuizId)
            .HasConstraintName("FK_CategoriesQuizzes_QuizId_Quizzes_Id");
    }
}