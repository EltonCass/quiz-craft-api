using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using MapsterMapper;
using NSubstitute;
using QuizCraft.Application.Categories;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.Tests;

public class CategoryHandlerTests
{
    private ICategoryRepository _repository;
    private IMapper _mapper;
    private IValidator<CategoryForUpsert> _validator;

    public CategoryHandlerTests()
    {
        _repository = Substitute.For<ICategoryRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<IValidator<CategoryForUpsert>>();
    }

    [Fact]
    public void CategoryHandler_WhenDependenciesAreNull_ThrowsException()
    {
        // Arrange
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        _mapper = null;
        _validator = null;
        _repository = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Act
        var callback = HandlerInstace;

        // Assert
        callback.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task RetrieveCategory_WhenRepositoryReturnsCategory_ReturnsCategoryForDisplay()
    {
        // Arrange
        var categoryId = 1;
        var categoryRetrieved = new Category() { Id = categoryId };
        _repository.GetCategory(categoryId, default)
            .ReturnsForAnyArgs(categoryRetrieved);
        _mapper.Map<CategoryForDisplay>(categoryRetrieved)
            .Returns(new CategoryForDisplay(categoryId, "", ""));
        var handler = HandlerInstace();

        // Act
        var result = await handler
            .RetrieveCategory(categoryId, default);

        // Assert
        using AssertionScope scope = new();
        result.Value.Should().BeOfType<CategoryForDisplay>();
        result.AsT0.Id.Should().Be(categoryId);
    }

    [Fact]
    public async Task RetrieveCategory_WhenRepositoryReturnsError_ReturnsError()
    {
        // Arrange
        var categoryId = 1;
        _repository.GetCategory(categoryId, default)
            .Returns(new RequestError(
                System.Net.HttpStatusCode.BadRequest, ""));
        var handler = HandlerInstace();

        // Act
        var result = await handler
            .RetrieveCategory(categoryId, default);

        // Assert
        using AssertionScope scope = new();
        result.Value.Should().BeOfType<RequestError>();
        result.AsT1.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    private CategoryHandler HandlerInstace()
        => new(_validator, _repository, _mapper);
}
