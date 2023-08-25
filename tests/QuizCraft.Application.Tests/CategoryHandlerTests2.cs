using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using MapsterMapper;
using NSubstitute;
using QuizCraft.Application.Categories;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.Tests;

public class CategoryHandlerTests2
{
    private ICategoryRepository _repositoryWithResult;
    private ICategoryRepository _repositoryWithError;
    private ICategoryRepository _repository;
    private IMapper _mapper;
    private IValidator<CategoryForUpsert> _validator;

    public CategoryHandlerTests2()
    {
        //_repositoryWithResult = new StubRepository();
        //_repositoryWithError = new StubErrorRepository();
        _repository = A.Fake<ICategoryRepository>();
        _mapper = A.Fake<IMapper>();
        _validator = Substitute.For<IValidator<CategoryForUpsert>>();
    }

    [Fact]
    public async Task ACategoryHandler_WhenDependenciesAreNull_ThrowsException()
    {
        // Arrange
        _mapper = null;
        _validator = null;
        _repository = null;

        // Act
        var callback = () => HandlerInstace();

        // Assert
        callback.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task ARetrieveCategory_WhenRepositoryReturnsCategory_ReturnsCategoryForDisplay()
    {
        // Arrange
        var category = 1;
        A.CallTo(() => _repository.GetCategory(category, default))
            .Returns(new Category());
        A.CallTo(() => _mapper.Map<CategoryForDisplay>(new Category() { Id = 1 }))
            .Returns(new CategoryForDisplay(default, "", ""));

        var handler = HandlerInstace();

        // Act
        var result = await handler
            .RetrieveCategory(category, default);

        // Assert
        result.IsT0.Should().BeTrue($"Result is {nameof(result.Value)}");
    }

    [Fact]
    public async Task ARetrieveCategory_WhenRepositoryReturnsError_ReturnsError()
    {
        // Arrange
        var category = 1;
        A.CallTo(() => _repository.GetCategory(category, default))
            .Returns(new RequestError(System.Net.HttpStatusCode.BadRequest, ""));
        var handler = HandlerInstace();

        // Act
        var result = await handler
            .RetrieveCategory(category, default);

        // Assert
        result.IsT1.Should().BeTrue($"Result is {nameof(result.Value)}");
    }

    private CategoryHandler HandlerInstace()
        => new CategoryHandler(_validator, _repository, _mapper);
}
