﻿// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.Quizzes.Questions;

public interface IFillInBlankQuestionRepository
{
    Task<OneOf<FillInBlankQuestion, RequestError>> CreateQuestion(
        FillInBlankQuestion question, CancellationToken cancellationToken);

    Task<OneOf<FillInBlankQuestion, RequestError>> GetQuestion(
        int quizId, int questionId, CancellationToken cancellationToken);

    Task<ICollection<FillInBlankQuestion>> GetQuestions(
        int quizId, CancellationToken cancellationToken);

    Task<OneOf<FillInBlankQuestion, RequestError>> UpdateQuestion(
        int questionId, FillInBlankQuestion updatedquestion, CancellationToken cancellationToken);
}
