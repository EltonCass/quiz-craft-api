// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using OneOf;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement;

public class QuizRepository : IQuizRepository
{
    private readonly IValidator<Quiz> _validator;

    public QuizRepository(IValidator<Quiz> validator)
    {
        ArgumentNullException.ThrowIfNull(validator);
        _validator = validator;
    }

    public Task<OneOf<Quiz, RequestError>> CreateQuiz(Quiz newQuiz, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<Quiz, RequestError>> DeleteQuiz(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<Quiz, RequestError>> RetrieveQuiz(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public async Task<IEnumerable<Quiz>> RetrieveQuizzes(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        return QuizzesStub.Records.ToList();
    }

    public Task<OneOf<Quiz, RequestError>> UpdateQuiz(Quiz quiz, CancellationToken cancellationToken) => throw new NotImplementedException();
}
