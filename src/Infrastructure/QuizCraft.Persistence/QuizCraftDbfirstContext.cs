using Microsoft.EntityFrameworkCore;
using QuizCraft.Persistence.Models;

namespace QuizCraft.Persistence;

public partial class QuizCraftDbfirstContext : DbContext
{
    public QuizCraftDbfirstContext()
    {
    }

    public QuizCraftDbfirstContext(DbContextOptions<QuizCraftDbfirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriesQuiz> CategoriesQuizzes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<FillInBlankQuestion> FillInBlankQuestions { get; set; }

    public virtual DbSet<MultipleOptionQuestion> MultipleOptionQuestions { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizCraftDbfirstContext).Assembly);
    }
}
