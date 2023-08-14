using QuizCraft.Models.Entities;

namespace QuizCraft.Models.DTOs;

public record CategoryDTO(int Id, string Name, string Description)
{
    public static Category ToEntity(CategoryDTO newCategory) =>
        new Category
        {
            Id = newCategory.Id,
            Name = newCategory.Name,
            Description = newCategory.Description,
        };
}
