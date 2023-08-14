﻿// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.QuizManagement.QuestionManagement;

public interface IUpsertQuestionRepository<T> where T : QuestionDTO
{
    Task<OneOf<T, RequestError>> CreateQuestion(
        int quizId, T newQuestion, CancellationToken cancellationToken);
    Task<OneOf<T, RequestError>> UpdateQuestion(
        int quizId, int questionId, T question, CancellationToken cancellationToken);
}