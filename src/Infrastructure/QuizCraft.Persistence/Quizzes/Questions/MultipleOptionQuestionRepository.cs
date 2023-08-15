﻿// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Application.QuizManagement.QuestionManagement;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Quizzes.Questions;

public class MultipleOptionQuestionRepository : IMultipleOptionQuestionRepository
{
    public Task<OneOf<MultipleOptionQuestion, RequestError>> CreateQuestion(MultipleOptionQuestion question, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<MultipleOptionQuestion, RequestError>> DeleteQuestion(int questionId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<MultipleOptionQuestion, RequestError>> GetQuestion(int questionId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<ICollection<MultipleOptionQuestion>> GetQuestions(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<MultipleOptionQuestion, RequestError>> UpdateQuestion(int questionId, MultipleOptionQuestion updatedquestion, CancellationToken cancellationToken) => throw new NotImplementedException();
}