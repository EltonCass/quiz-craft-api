// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using System.Net;

namespace QuizCraft.Models;

public record RequestError(HttpStatusCode StatusCode, string Message);