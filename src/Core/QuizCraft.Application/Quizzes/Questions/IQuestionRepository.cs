// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.Quizzes.Questions;

public interface IQuestionRepository
{
    Task<OneOf<Question, RequestError>> DeleteQuestion(
        int quizId, int questionId, CancellationToken cancellationToken);
    Task<OneOf<Question, RequestError>> GetQuestion(
        int quizId, int questionId, CancellationToken cancellationToken);
    Task<ICollection<Question>> GetQuestions(
        int quizId, CancellationToken cancellationToken);
}
