// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.QuizManagement;

public interface IQuizRepository
{
    Task<IEnumerable<QuizDTO>> RetrieveQuizzes(CancellationToken cancellationToken);
    Task<OneOf<QuizDTO, RequestError>> RetrieveQuiz(int id, CancellationToken cancellationToken);
    Task<OneOf<QuizDTO, RequestError>> CreateQuiz(QuizDTO newQuiz, CancellationToken cancellationToken);
    Task<OneOf<QuizDTO, RequestError>> UpdateQuiz(QuizDTO quiz, CancellationToken cancellationToken);
    Task<OneOf<QuizDTO, RequestError>> DeleteQuiz(int id, CancellationToken cancellationToken);
}
