// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.Entities;
using System.Net;

namespace QuizCraft.Application.QuizManagement.QuestionManagement;

public class QuestionRepository : IQuestionRepository
{
    public Task<OneOf<FillInBlankQuestion, RequestError>> CreateFillInBlankQuestion(int quizId, FillInBlankQuestion newQuestion, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<MultipleOptionQuestion, RequestError>> CreateMultipleOptionQuestion(int quizId, MultipleOptionQuestion newQuestion, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<BaseQuestion, RequestError>> DeleteQuestions(int quizId, int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<BaseQuestion, RequestError>> RetrieveQuestion(int quizId, int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var foundedQuiz = QuizzesStub.Records
        //    .FirstOrDefault(q => q.Id == quizId);
        //if (foundedQuiz is null)
        //{
        //    return NotFound();
        //}

        //var foundedQuestion = foundedQuiz.Questions
        //    .FirstOrDefault(q => q.Id == questionId);
        //if (foundedQuestion is null)
        //{
        //    return NotFound();
        //}
    }

    public async Task<OneOf<IEnumerable<BaseQuestion>, RequestError>> RetrieveQuestions(int quizId, CancellationToken cancellationToken)
    {
        var foundedQuiz = QuizzesStub.Records.FirstOrDefault(q => q.Id == quizId);
        await Task.Delay(100, cancellationToken);
        if (foundedQuiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, "Quiz does not exists.");
        }

        return foundedQuiz.Questions.ToList();
    }

    public Task<OneOf<FillInBlankQuestion, RequestError>> UpdateFillInBlankQuestion(int quizId, FillInBlankQuestion newQuestion, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<MultipleOptionQuestion, RequestError>> UpdateMultipleOptionQuestion(int quizId, MultipleOptionQuestion newQuestion, CancellationToken cancellationToken) => throw new NotImplementedException();
}
