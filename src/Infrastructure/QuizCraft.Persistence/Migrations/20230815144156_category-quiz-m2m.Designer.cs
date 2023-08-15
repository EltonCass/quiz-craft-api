﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizCraft.Persistence;

#nullable disable

namespace QuizCraft.Persistence.Migrations
{
    [DbContext(typeof(QuizCraftContext))]
    [Migration("20230815144156_category-quiz-m2m")]
    partial class categoryquizm2m
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryQuiz", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("QuizzesId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "QuizzesId");

                    b.HasIndex("QuizzesId");

                    b.ToTable("CategoryQuiz");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_Categories_Id");

                    b.HasIndex(new[] { "Name" }, "UQ_Categories_Name")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Quizzes Related to C# Programming",
                            Name = "C# Programming"
                        });
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.FillInBlankQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("WordPosition")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_FillInBlankQuestions_Id");

                    b.HasIndex("QuestionId")
                        .IsUnique();

                    b.ToTable("FillInBlankQuestions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            QuestionId = 2,
                            WordPosition = 4
                        });
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.MultipleOptionQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_MultipleOptionQuestions_Id");

                    b.HasIndex("QuestionId")
                        .IsUnique();

                    b.ToTable("MultipleOptionQuestions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            QuestionId = 1
                        });
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MultipleOptionQuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id")
                        .HasName("PK_Options_Id");

                    b.HasIndex("MultipleOptionQuestionId");

                    b.ToTable("Options");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MultipleOptionQuestionId = 1,
                            Text = "Common Language Runtime"
                        },
                        new
                        {
                            Id = 2,
                            MultipleOptionQuestionId = 1,
                            Text = "Core Language Runtime"
                        },
                        new
                        {
                            Id = 3,
                            MultipleOptionQuestionId = 1,
                            Text = "Compiled Language Runtime"
                        });
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("PlacementOrder")
                        .HasColumnType("int");

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("PK_Questions_Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CorrectAnswer = "Common Language Runtime",
                            CreatedAt = new DateTime(2023, 8, 15, 10, 41, 56, 83, DateTimeKind.Local).AddTicks(2193),
                            QuizId = 1,
                            Text = "What does the acronym \"CLR\" stand for in the context of C# and .NET?"
                        },
                        new
                        {
                            Id = 2,
                            CorrectAnswer = "property",
                            CreatedAt = new DateTime(2023, 8, 15, 10, 41, 56, 83, DateTimeKind.Local).AddTicks(2208),
                            QuizId = 1,
                            Text = "In C#, a ______ is a class member that encapsulates a get accessor and an optional set accessor to provide controlled access to an object's state"
                        });
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("UpdatedByUserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_Quizzes_Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("Quizzes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 8, 15, 14, 41, 56, 83, DateTimeKind.Utc).AddTicks(2109),
                            CreatedByUserId = 1,
                            Description = "This Quiz contains basic questions about C#.",
                            Title = "C# Fundamentals"
                        });
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id")
                        .HasName("PK_Users_Id");

                    b.HasIndex(new[] { "Email" }, "UQ_Users_Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            FullName = "Admin"
                        });
                });

            modelBuilder.Entity("CategoryQuiz", b =>
                {
                    b.HasOne("QuizCraft.Models.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuizCraft.Models.Entities.Quiz", null)
                        .WithMany()
                        .HasForeignKey("QuizzesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.FillInBlankQuestion", b =>
                {
                    b.HasOne("QuizCraft.Models.Entities.Question", "Question")
                        .WithOne("FillInBlankQuestion")
                        .HasForeignKey("QuizCraft.Models.Entities.FillInBlankQuestion", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FillInBlankQuestions_QuestionId_Questions_Id");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.MultipleOptionQuestion", b =>
                {
                    b.HasOne("QuizCraft.Models.Entities.Question", "Question")
                        .WithOne("MultipleOptionQuestion")
                        .HasForeignKey("QuizCraft.Models.Entities.MultipleOptionQuestion", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MultipleOptionQuestions_QuestionId_Questions_Id");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Option", b =>
                {
                    b.HasOne("QuizCraft.Models.Entities.MultipleOptionQuestion", "MultipleOptionQuestion")
                        .WithMany("Options")
                        .HasForeignKey("MultipleOptionQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Options_MultipleOptionQuestionId_MultipleOptionQuestions_Id");

                    b.Navigation("MultipleOptionQuestion");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Question", b =>
                {
                    b.HasOne("QuizCraft.Models.Entities.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Questions_QuizId_Quizzes_Id");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Quiz", b =>
                {
                    b.HasOne("QuizCraft.Models.Entities.User", "CreatedByUser")
                        .WithMany("QuizzesCreatedBy")
                        .HasForeignKey("CreatedByUserId")
                        .HasConstraintName("FK_Quizzes_CreatedByUserId_Users_Id");

                    b.HasOne("QuizCraft.Models.Entities.User", "UpdatedByUser")
                        .WithMany("QuizzesUpdatedBy")
                        .HasForeignKey("UpdatedByUserId")
                        .HasConstraintName("FK_Quizzes_UpdatedByUserId_Users_Id");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.MultipleOptionQuestion", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Question", b =>
                {
                    b.Navigation("FillInBlankQuestion");

                    b.Navigation("MultipleOptionQuestion");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.Quiz", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QuizCraft.Models.Entities.User", b =>
                {
                    b.Navigation("QuizzesCreatedBy");

                    b.Navigation("QuizzesUpdatedBy");
                });
#pragma warning restore 612, 618
        }
    }
}
