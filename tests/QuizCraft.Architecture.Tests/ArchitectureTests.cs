// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentAssertions;
using NetArchTest.Rules;

namespace QuizCraft.Architecture.Tests;

public class ArchitectureTests
{
    [Theory]
    [InlineData(typeof(Infrastructure.AssemblyReference))]
    [InlineData(typeof(Persistence.AssemblyReference))]
    [InlineData(typeof(Api.AssemblyReference))]
    [InlineData(typeof(Application.AssemblyReference))]
    public void DomainModels_ShouldNotHaveOtherDependency(Type projectType)
    {
        var assembly = typeof(Models.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn(projectType.Namespace)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [InlineData(typeof(Infrastructure.AssemblyReference), true)]
    [InlineData(typeof(Persistence.AssemblyReference), true)]
    [InlineData(typeof(Api.AssemblyReference), true)]
    [InlineData(typeof(Models.AssemblyReference), false)]
    public void DomainApplication_ShouldNotHaveOtherDependenciesButDomainModels(
        Type projectType, bool expectedResult)
    {
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn(projectType.Namespace)
            .GetResult();

        if (expectedResult)
        {
            testResult.IsSuccessful.Should().BeTrue();
            return;
        }

        testResult.IsSuccessful.Should().BeFalse();
    }

    [Theory]
    [InlineData(typeof(Persistence.AssemblyReference), true)]
    [InlineData(typeof(Api.AssemblyReference), true)]
    [InlineData(typeof(Models.AssemblyReference), false)]
    [InlineData(typeof(Application.AssemblyReference), false)]
    public void Infrastructure_ShouldNotHaveOtherDependenciesButDomain(
        Type projectType, bool expectedResult)
    {
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn(projectType.Namespace)
            .GetResult();

        if (expectedResult)
        {
            testResult.IsSuccessful.Should().BeTrue();
            return;
        }

        testResult.IsSuccessful.Should().BeFalse();
    }
}