using FluentAssertions;
using QuizCraft.Application.Categories;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Tests;

public class CategoryValidatorTests
{
    [Fact]
    public void ShouldHaveValidationError_WhenNameIsEmpty()
    {
        // Arrange
        var category = new CategoryForUpsert("", "");
        var validator = new CategoryValidator();

        // Act
        var validationResult = validator.Validate(category);

        // Assert
        validationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public void ShouldBeValid_WhenCategoryIsValid()
    {
        // Arrange
        var category = new CategoryForUpsert(
            "Programming", "Contains quizzes about programming.");
        var validator = new CategoryValidator();

        // Act
        var validationResult = validator.Validate(category);

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }
}