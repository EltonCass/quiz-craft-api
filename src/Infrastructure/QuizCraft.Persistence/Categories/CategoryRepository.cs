// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using QuizCraft.Application.CategoryManagement;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly QuizCraftContext _Context;

    public CategoryRepository(QuizCraftContext context)
    {
        _Context = context;
    }

    public async Task<Category> CreateCategory(Category category, CancellationToken cancellationToken)
    {
        await _Context.Categories
            .AddAsync(category, cancellationToken);
        var result = _Context.SaveChanges();

        if (result == 0)
        {
            // Handle case when upsert did not work
        }

        return category;
    }

    public Task<Category> DeleteCategory(int categoryId, CancellationToken cancellationToken) => throw new NotImplementedException();
    
    public async Task<ICollection<Category>> GetCategories(CancellationToken cancellationToken)
    {
        return await _Context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken)
    {
        var category = await _Context.Categories
            .Where(x => x.Id == categoryId)
            .FirstOrDefaultAsync(cancellationToken) ?? new Category();
        var debugView = _Context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
        return category;
    }

    public Task<Category> UpdateCategory(int categoryId, Category updatedCategory, CancellationToken cancellationToken) => throw new NotImplementedException();
}
