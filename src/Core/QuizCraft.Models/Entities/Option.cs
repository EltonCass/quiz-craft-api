// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities
{
    public class Option : IEntity
    {
        public Option()
        {
        }

        public int Id { get; set; }
        public int MultipleOptionQuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public MultipleOptionQuestion? MultipleOptionQuestion { get; set; }
    }
}