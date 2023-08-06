// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using OneOf;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Models.Entities;
using System.Net;

namespace QuizCraft.Application.CategoryManagement;

public class CategoryRepository : ICategoryRepository
{
    private readonly IValidator<Category> _validator;

    public CategoryRepository(IValidator<Category> validator)
    {
        ArgumentNullException.ThrowIfNull(validator);
        _validator = validator;
    }

    public async Task<OneOf<Category, RequestError>> CreateCategory(Category newCategory, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(newCategory);
        await Task.Delay(100, cancellationToken);
        if (result.IsValid)
        {
            return newCategory;
        }

        return new RequestError(HttpStatusCode.UnprocessableEntity,
            string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
    }
    public async Task<OneOf<Category, RequestError>> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var foundedQuiz = QuizzesStub.Records
            .FirstOrDefault(r => r.Categories
                .Select(c => c.Id)
                .Contains(id));

        if (foundedQuiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, "Quiz does not exist");
        }

        Category? foundedCategory = null;

        foreach (var category in foundedQuiz.Categories)
        {
            if (category.Id == id)
            {
                foundedCategory = category;
                // TODO logic to erase category from quiz and category itself
                break;
            }
        }

        if (foundedCategory is null)
        {
            return new RequestError(HttpStatusCode.NotFound, "Category does not exist");
        }

        await Task.Delay(100, cancellationToken);
        return foundedCategory;
    }

    public Task<IEnumerable<Quiz>> RetrieveCategories(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<Category, RequestError>> RetrieveCategory(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<Category, RequestError>> UpdateCategory(Category category, CancellationToken cancellationToken) => throw new NotImplementedException();
}
