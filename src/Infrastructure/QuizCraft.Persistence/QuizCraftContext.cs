using Microsoft.EntityFrameworkCore;
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

        public DbSet<CategoriesQuiz> CategoriesQuizzes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FillInBlankQuestion> FillInBlankQuestions { get; set; }
        public DbSet<MultipleOptionQuestion> MultipleOptionQuestions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
