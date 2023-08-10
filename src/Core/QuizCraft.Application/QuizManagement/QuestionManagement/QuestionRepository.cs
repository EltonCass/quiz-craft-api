// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.DTOs;
using System.Net;

namespace QuizCraft.Application.QuizManagement.QuestionManagement;

public class QuestionRepository : IQuestionRepository
{
    public async Task<OneOf<BaseQuestion, RequestError>> DeleteQuestion(int quizId, int id, CancellationToken cancellationToken)
    {
        var foundedQuiz = Stubs.Quizzes.FirstOrDefault(q => q.Id == quizId);
        await Task.Delay(100, cancellationToken);
        if (foundedQuiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }

        var foundedQuestion = foundedQuiz.Questions.FirstOrDefault(q => q.Id == id);
        if (foundedQuestion is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuestionNotFound);
        }

        foundedQuiz.Questions.Remove(foundedQuestion);
        return foundedQuestion;
    }

    public async Task<OneOf<BaseQuestion, RequestError>> RetrieveQuestion(int quizId, int id, CancellationToken cancellationToken)
    {
        var foundedQuiz = Stubs.Quizzes.FirstOrDefault(q => q.Id == quizId);
        await Task.Delay(100, cancellationToken);
        if (foundedQuiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }

        var foundedQuestion = foundedQuiz.Questions.FirstOrDefault(q => q.Id == id);
        if (foundedQuestion is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuestionNotFound);
        }

        return foundedQuestion;
    }

    public async Task<OneOf<IEnumerable<BaseQuestion>, RequestError>> RetrieveQuestions(int quizId, CancellationToken cancellationToken)
    {
        var foundedQuiz = Stubs.Quizzes.FirstOrDefault(q => q.Id == quizId);
        await Task.Delay(100, cancellationToken);
        if (foundedQuiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }

        return foundedQuiz.Questions.ToList();
    }
}
