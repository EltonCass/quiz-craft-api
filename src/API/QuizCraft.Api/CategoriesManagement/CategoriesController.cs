// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.CategoryManagement;
using QuizCraft.Models.Entities;

namespace QuizCraft.Api.CategoriesManagement;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository category)
    {
        ArgumentNullException.ThrowIfNull(category);
        _categoryRepository = category;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories(CancellationToken cancellationToken)
    {
        return Ok(await _categoryRepository.RetrieveCategories(cancellationToken));
    }

    [HttpGet("{id}", Name = "GetCategory")]
    [ProducesResponseType(typeof(Category), 200)]
    public async Task<ActionResult<Category>> GetCategory(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.RetrieveCategory(id, cancellationToken);

        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Category), 201)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<Category>> PostCategory([FromBody] Category category, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.CreateCategory(category, cancellationToken);
        if (result.IsT0)
        {
            return Created("GetCategory", result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Category), 200)]
    [ProducesResponseType(422)]
    public async Task<ActionResult> PutCategory(int id, [FromBody] Category category, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.UpdateCategory(id, category, cancellationToken);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public async Task<ActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.DeleteCategory(id, cancellationToken);
        if (result.IsT0)
        {
            return NoContent();
        }

        return result.HandleError(this);
    }
}
