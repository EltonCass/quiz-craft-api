using OneOf;
using QuizCraft.Application.Categories;
using QuizCraft.Models;
using QuizCraft.Models.Entities;
using System.Net;

namespace QuizCraft.Application.Tests.TestDoubles;

internal class StubErrorRepository : ICategoryRepository
{
    public Task<OneOf<ICollection<Category>, RequestError>> AddCategoriesByNames(
        ICollection<Category> categories, CancellationToken cancellationToken)
        => throw new NotImplementedException();
    public Task<OneOf<Category, RequestError>> CreateCategory(
        Category category, CancellationToken cancellationToken) =>
        Task.FromResult(OneOf<Category, RequestError>.FromT1(
            new RequestError(HttpStatusCode.BadRequest, "")));
    public Task<OneOf<Category, RequestError>> DeleteCategory(
        int categoryId, CancellationToken cancellationToken) =>
        Task.FromResult(OneOf<Category, RequestError>.FromT1(
            new RequestError(HttpStatusCode.BadRequest, "")));
    public Task<ICollection<Category>> GetCategories(
        CancellationToken cancellationToken) =>
        Task.FromResult((ICollection<Category>)new List<Category>());

    public Task<(ICollection<Category> MatchingCategories, ICollection<Category> NonMatchingCategories)>
        GetCategoriesByNames(string[] categories, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<OneOf<Category, RequestError>> GetCategory(
        int categoryId, CancellationToken cancellationToken) =>
        Task.FromResult(OneOf<Category, RequestError>.FromT1(
            new RequestError(HttpStatusCode.BadRequest, "")));

    public Task<OneOf<Category, RequestError>> UpdateCategory(
        Category updatedCategory, CancellationToken cancellationToken) =>
        Task.FromResult(OneOf<Category, RequestError>.FromT1(
            new RequestError(HttpStatusCode.BadRequest, "")));
}
