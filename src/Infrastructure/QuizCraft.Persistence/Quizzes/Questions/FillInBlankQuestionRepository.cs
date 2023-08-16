// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Quizzes.Questions;

public class FillInBlankQuestionRepository : IFillInBlankQuestionRepository
{
    public Task<OneOf<FillInBlankQuestion, RequestError>> CreateQuestion(FillInBlankQuestion question, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<FillInBlankQuestion, RequestError>> DeleteQuestion(int questionId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<FillInBlankQuestion, RequestError>> GetQuestion(int questionId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<ICollection<FillInBlankQuestion>> GetQuestions(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<FillInBlankQuestion, RequestError>> UpdateQuestion(int questionId, FillInBlankQuestion updatedquestion, CancellationToken cancellationToken) => throw new NotImplementedException();
}
