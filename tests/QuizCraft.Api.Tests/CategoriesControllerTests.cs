// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using QuizCraft.Api.Categories;
using QuizCraft.Application.Categories;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.Tests;

public class CategoriesControllerTests
{
    private ICategoryHandler _handler;
    private IMapper _mapper;

    public CategoriesControllerTests()
    {
        _handler = Substitute.For<ICategoryHandler>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task GetCategory_WhenCategoryExist_ReturnsSuccessfulState()
    {
        //Arrange
        var categoryId = 1;
        _handler.RetrieveCategory(categoryId, default)
            .Returns(new CategoryForDisplay(1, "", ""));

        var controller = ControllerInstance();

        //Act
        var result = await controller
            .GetCategory(categoryId, default);

        //Assert
        ((OkObjectResult)result.Result).StatusCode.Should().Be(200);
    }

    private CategoriesController ControllerInstance() =>
        new CategoriesController(_handler, _mapper);
}