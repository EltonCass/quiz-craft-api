// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Api.Models
{
    public record FillInResponse(
        string QuestionContent, string BlankWord, int ocurrence);
}
