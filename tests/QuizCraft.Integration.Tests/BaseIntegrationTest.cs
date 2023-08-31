using Microsoft.Extensions.DependencyInjection;
using QuizCraft.Persistence;

namespace QuizCraft.Integration.Tests;

public class BaseIntegrationTest : IClassFixture<TestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly QuizCraftContext DbContext;
    protected readonly HttpClient Client;

    public BaseIntegrationTest(TestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<QuizCraftContext>();
        Client = factory.CreateClient();
    }
}
