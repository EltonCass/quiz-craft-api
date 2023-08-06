// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Application.CategoryManagement;
using QuizCraft.Models.Entities;

namespace QuizCraft.Api.CategoriesManagement;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _category;

    public CategoriesController(ICategoryRepository category)
    {
        ArgumentNullException.ThrowIfNull(category);
        _category = category;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken)
    {
        return Ok(_category.RetrieveCategories(cancellationToken));
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<CategoriesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<CategoriesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CategoriesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
