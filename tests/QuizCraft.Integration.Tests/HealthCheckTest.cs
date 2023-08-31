namespace QuizCraft.Integration.Tests;

public class HealthCheckTest : BaseIntegrationTest
{
    public HealthCheckTest(TestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task HealthCheck_ReturnsOk()
    {
        var response = await Client.GetAsync("/health");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", responseString);
    }
}
