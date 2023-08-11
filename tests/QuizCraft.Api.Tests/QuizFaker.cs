// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Bogus;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.Tests;

public class QuizFaker : Faker<Quiz>
{
    public QuizFaker() 
    {
        //RuleFor(a => a.Categories, f => f.Commerce.Categories(1)[1]);
        //RuleFor(a => a.Questions, f => f.makMake<IQuestion>(
        //    f.Random.Int(2, 5),
        //    () => new MultipleOptionQuestion(
        //        f.Lorem.Word(),new List<string>(),f.Lorem.Word())
    }
}
