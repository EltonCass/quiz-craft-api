using FluentAssertions;
using FluentValidation;
using MapsterMapper;
using NSubstitute;
using QuizCraft.Application.Categories;
using QuizCraft.Application.Tests.TestDoubles;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Tests;

public class CategoryHandlerTests
{
    private ICategoryRepository _repositoryWithResult;
    private ICategoryRepository _repositoryWithError;
    private IMapper _mapper;
    private IValidator<CategoryForUpsert> _validator;

    public CategoryHandlerTests()
    {
        _repositoryWithResult = new StubRepository();
        _repositoryWithError = new StubErrorRepository();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<IValidator<CategoryForUpsert>>();
    }

    [Fact]
    public async Task CategoryHandler_WhenDependenciesAreNull_ThrowsException()
    {
        // Arrange
        _mapper = null;
        _validator = null;
        _repositoryWithResult = null;

        // Act
        var callback = () => HandlerInstace(_repositoryWithResult);

        // Assert
        callback.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task RetrieveCategory_WhenRepositoryReturnsCategory_ReturnsCategoryForDisplay()
    {
        // Arrange
        _mapper.Map<CategoryForDisplay>(
            await _repositoryWithResult.GetCategory(default, default))
            .Returns(new CategoryForDisplay(default, "", ""));
        var handler = HandlerInstace(_repositoryWithResult);

        // Act
        var result = await handler
            .RetrieveCategory(Arg.Any<int>(), default);

        // Assert
        result.IsT0.Should().BeTrue($"Result is {nameof(result.Value)}");
    }

    [Fact]
    public async Task RetrieveCategory_WhenRepositoryReturnsError_ReturnsError()
    {
        // Arrange
        var handler = HandlerInstace(_repositoryWithError);

        // Act
        var result = await handler
            .RetrieveCategory(Arg.Any<int>(), default);

        // Assert
        result.IsT1.Should().BeTrue($"Result is {nameof(result.Value)}");
    }

    private CategoryHandler HandlerInstace(ICategoryRepository repository)
        => new CategoryHandler(_validator, repository, _mapper);
}
