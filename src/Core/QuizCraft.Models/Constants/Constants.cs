// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Constants;

public static class Constants
{
    public static class RequestErrorMessages
    {
        public const string QuizNotFound = "Quiz was not found.";
        public const string QuestionNotFound = "Question was not found.";
        public const string NoChanges = "No changes were applied.";
        public const string CategoryNotFound = "Category was not found.";
    }

    public static class ValidationMessages
    {
        public const string NameNotUnique = "Name must be unique.";
    }
}
