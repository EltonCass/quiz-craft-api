// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;

namespace QuizCraft.Api.CategoriesManagement;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> GetCategories()
    {
        return new string[] { "value1", "value2" };
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
