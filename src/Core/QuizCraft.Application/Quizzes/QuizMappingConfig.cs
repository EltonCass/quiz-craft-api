// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Mapster;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.Quizzes;

public class QuizMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Quiz, QuizDTO>()
            .MapWith(src => new QuizDTO(
                src.Id,
                src.Categories.Adapt<CategoryForDisplay[]>(),
                src.Description,
                src.Title,
                src.Questions.Adapt<QuestionDTO[]>(),
                src.CreatedAt,
                src.UpdatedAt,
                src.CreatedByUserId));

        config.NewConfig<QuizDTO, Quiz>()
            .Map(dest => dest.Categories, src => src.Categories)
            .Ignore(dest => dest.Questions);
    }
}
