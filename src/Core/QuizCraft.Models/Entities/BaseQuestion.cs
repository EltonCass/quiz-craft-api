// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Constants;

namespace QuizCraft.Models.Entities;

public record BaseQuestion(int Id, string CorrectAnswer, int Score, string Text, QuestionType Type);