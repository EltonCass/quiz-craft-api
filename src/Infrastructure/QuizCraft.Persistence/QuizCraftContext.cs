﻿using Microsoft.EntityFrameworkCore;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence
{
    public class QuizCraftContext : DbContext
    {
        public QuizCraftContext()
        {
        }

        public QuizCraftContext(DbContextOptions<QuizCraftContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<FillInBlankQuestion> FillInBlankQuestions { get; set; }
        public DbSet<MultipleOptionQuestion> MultipleOptionQuestions { get; set; }
        public DbSet<Option> Options { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(QuizCraftContext).Assembly);

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    FullName = "Admin"
                });

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "C# Programming",
                    Description = "Quizzes Related to C# Programming"
                });

            modelBuilder.Entity<Quiz>().HasData(
                new Quiz()
                {
                    Id = 1,
                    Title = "C# Fundamentals",
                    Description = "This Quiz contains basic questions about C#.",
                    CreatedAt = DateTime.UtcNow,
                    CreatedByUserId = 1,
                });

            modelBuilder.Entity<Question>().HasData(
                new Question[]
                {
                    new Question{
                        Id = 1,
                        CorrectAnswer = "Common Language Runtime",
                        Text = "What does the acronym \"CLR\" stand for in the context of C# and .NET?",
                        CreatedAt = DateTime.Now,
                        QuizId = 1,
                    },
                    new Question()
                    {
                        Id = 2,
                        CorrectAnswer = "property",
                        Text = "In C#, a ______ is a class member that encapsulates a get accessor and an optional set accessor to provide controlled access to an object's state",
                        CreatedAt = DateTime.Now,
                        QuizId = 1,
                    }
                });

            modelBuilder.Entity<MultipleOptionQuestion>().HasData(
                new MultipleOptionQuestion()
                {
                    Id = 1,
                    QuestionId = 1
                });

            modelBuilder.Entity<FillInBlankQuestion>().HasData(
                new FillInBlankQuestion()
                {
                    Id = 1,
                    QuestionId = 2,
                    WordPosition = 4
                });

            modelBuilder.Entity<Option>().HasData(
                new Option()
                {
                    Id = 1,
                    MultipleOptionQuestionId = 1,
                    Text = "Common Language Runtime",
                },
                new Option()
                {
                    Id = 2,
                    MultipleOptionQuestionId = 1,
                    Text = "Core Language Runtime",
                },
                new Option()
                {
                    Id = 3,
                    MultipleOptionQuestionId = 1,
                    Text = "Compiled Language Runtime",
                });

        }
    }
}
