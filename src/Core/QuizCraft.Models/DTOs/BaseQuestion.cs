// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Constants;

namespace QuizCraft.Models.DTOs;

public record BaseQuestion(
    int Id, int QuizId, string CorrectAnswer, string Text, QuestionType Type, int? Score = null, short? Order = null);