// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities
{
    public class Option
    {
        public Option()
        {
            MultipleOptionQuestion = new();
        }

        public int Id { get; set; }
        public int MultipleOptionQuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public MultipleOptionQuestion MultipleOptionQuestion { get; set; }
    }
}