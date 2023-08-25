using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using QuizCraft.Models.Entities;
using QuizCraft.Persistence.Categories;

namespace QuizCraft.Persistence.Tests
{
    public class CategoryRepositoryTests
    {
        private IValidator<Category> _validator;

        public CategoryRepositoryTests()
        {
            _validator = Substitute.For<IValidator<Category>>();
        }

        [Fact]
        public async Task GetCategory_WhenCategoryExists_ReturnsCategory()
        {
            // Arrange
            var dbContextOptions = CreateNewContextOptions();
            using var context = new QuizCraftContext(dbContextOptions);
            var categoryService = new CategoryRepository(
                context, _validator);
            await context.Categories.AddAsync(new Category()
            {
                Id = 1, Description = "Test Category", Name = "Test Category"
            });
            await context.SaveChangesAsync();

            var category = 1;

            // Act
            var categoryResult = await categoryService
                .GetCategory(category, default);

            // Assert
            categoryResult.IsT0.Should().BeTrue();
        }

        [Fact]
        public async Task GetCategory_WhenCategoryDoesNotExists_ReturnsError()
        {
            // Arrange
            var dbContextOptions = CreateNewContextOptions();
            using var context = new QuizCraftContext(dbContextOptions);
            var category = int.MaxValue;
            var categoryService = new CategoryRepository(
                context, _validator);

            // Act
            var categoryResult = await categoryService
                .GetCategory(category, default);

            // Assert
            categoryResult.IsT1.Should().BeTrue();
        }

        private DbContextOptions<QuizCraftContext> CreateNewContextOptions()
        {
            // Use in-memory database for testing
            var optionsBuilder =
                new DbContextOptionsBuilder<QuizCraftContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging();

            return optionsBuilder.Options;
        }
    }
}