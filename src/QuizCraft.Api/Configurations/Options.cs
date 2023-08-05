
// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using System.ComponentModel.DataAnnotations;

namespace QuizCraft.Api.Configurations
{
    public class Options
    {
        public string OpenAIKey { get; set; }
        public AuthenticationConfiguration AuthenticationConfiguration { get; set; }
    }
}
