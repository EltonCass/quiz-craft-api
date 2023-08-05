﻿// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models;

public record MultipleOptionResponse(
    string QuestionContent, IEnumerable<string> Options);
