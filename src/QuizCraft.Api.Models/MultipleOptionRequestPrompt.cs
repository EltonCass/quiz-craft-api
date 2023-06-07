// Copyright (c) 2023  Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Api.Models
{
    public class MultipleOptionRequestPrompt
    {
        public required string PromptText { get; set; }
        public string? InformationSource { get; set; }
        public int OptionQuantity { get; set; }
    }
}
