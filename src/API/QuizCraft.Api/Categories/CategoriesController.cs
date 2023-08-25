// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.Categories;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.Categories;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryHandler _categoryHandler;
    private readonly IMapper _Mapper;

    public CategoriesController(ICategoryHandler category, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(category);
        ArgumentNullException.ThrowIfNull(mapper);
        _categoryHandler = category;
        _Mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryForDisplay>), 200)]
    public async Task<ActionResult<IEnumerable<CategoryForDisplay>>> GetCategories(
        CancellationToken cancellationToken)
    {
        return Ok(await _categoryHandler.RetrieveCategories(cancellationToken));
    }

    [HttpGet("{id}", Name = "GetCategory")]
    [ProducesResponseType(typeof(CategoryForDisplay), 200)]
    public async Task<ActionResult<CategoryForDisplay>> GetCategory(
        int id, CancellationToken cancellationToken)
    {
        var result = await _categoryHandler
            .RetrieveCategory(id, cancellationToken);

        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryForUpsert), 201)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<CategoryForUpsert>> PostCategory(
        [FromBody] CategoryForUpsert category, CancellationToken cancellationToken)
    {
        var result = await _categoryHandler
            .CreateCategory(category, cancellationToken);
        if (result.IsT0)
        {
            var resourceUrl = Url.Action(
                "GetCategory",
                "Categories",
                new { result.AsT0.Id, cancellationToken }, Request.Scheme);
            var responseCategory = _Mapper
                .Map<CategoryForUpsert>(result.AsT0);
            return Created(resourceUrl!, responseCategory);
        }

        return result.HandleError(this);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CategoryForDisplay), 200)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<CategoryForDisplay>> PutCategory(
        int id, [FromBody] CategoryForUpsert category, CancellationToken cancellationToken)
    {
        var result = await _categoryHandler
            .UpdateCategory(id, category, cancellationToken);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public async Task<ActionResult> DeleteCategory(
        int id, CancellationToken cancellationToken)
    {
        var result = await _categoryHandler
            .DeleteCategory(id, cancellationToken);
        if (result.IsT0)
        {
            return NoContent();
        }

        return result.HandleError(this);
    }
}
