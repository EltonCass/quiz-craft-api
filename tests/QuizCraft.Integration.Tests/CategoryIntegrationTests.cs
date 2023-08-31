using FluentAssertions;
using FluentAssertions.Execution;
using QuizCraft.Models.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace QuizCraft.Integration.Tests;

public class CategoryIntegrationTests : BaseIntegrationTest
{
    public CategoryIntegrationTests(TestWebAppFactory factory) : base(factory)
    {
    }

    [Fact(Skip = "Needs seeding")]
    public async Task GetCategories_ReturnsExpectedCategories()
    {
        var response = await Client.GetAsync("/api/v1/Categories");
        var categories = await response.Content
            .ReadFromJsonAsync<List<CategoryForDisplay>>();

        using var _ = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        categories.Should().BeEmpty();
    }
}