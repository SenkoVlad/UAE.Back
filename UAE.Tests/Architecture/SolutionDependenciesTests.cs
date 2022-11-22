using NetArchTest.Rules;
using Shouldly;
using UAE.Application;
using UAE.Core;
using UAE.Infrastructure;

namespace UAE.Tests.Architecture
{
    public class SolutionDependenciesTests
    {
        private const string ApplicationNamespace = "UAE.Application";
        private const string InfrastructureNamespace = "UAE.Infrastructure";
        private const string WebApiNamespace = "UAE.Api";

        [Fact]
        public void Core_Should_Not_Have_Dependencies_On_Other_Projects()
        {
            //Arrange
            var assembly = typeof(CoreAssembly).Assembly;
            
            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                WebApiNamespace
            };

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            //Assert
            testResult.IsSuccessful.ShouldBe(true);
        }
        
        [Fact]
        public void Application_Should_Not_Have_Dependencies_On_Other_Projects()
        {
            //Arrange
            var assembly = typeof(ApplicationAssembly).Assembly;
            
            var otherProjects = new[]
            {
                InfrastructureNamespace,
                WebApiNamespace
            };

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            //Assert
            testResult.IsSuccessful.ShouldBe(true);
        }
        
        [Fact]
        public void Infrastructure_Should_Not_Have_Dependencies_On_Other_Projects()
        {
            //Arrange
            var assembly = typeof(InfraAssembly).Assembly;
            
            var otherProjects = new[]
            {
                WebApiNamespace,
                InfrastructureNamespace
            };

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            //Assert
            testResult.IsSuccessful.ShouldBe(true);
        }
    }
}