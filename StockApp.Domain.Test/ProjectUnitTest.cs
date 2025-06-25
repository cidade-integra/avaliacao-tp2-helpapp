using FluentAssertions;
using StockApp.Domain.Entities;
using StockApp.Domain.Validation;

namespace StockApp.Domain.Test
{
    public class ProjectUnitTest
    {
        #region Testes Positivos

        [Fact]
        public void CreateProject_WithValidParameters_ShouldSucceed()
        {
            var project = new Project("Valid Project", "Some description", DateTime.Today, DateTime.Today.AddDays(10));

            project.Name.Should().Be("Valid Project");
            project.Description.Should().Be("Some description");
        }

        #endregion

        #region Testes Negativos

        [Fact]
        public void CreateProject_WithEmptyName_ShouldThrowDomainException()
        {
            Action action = () => new Project("", "desc", DateTime.Today, DateTime.Today.AddDays(1));
            action.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Project name is required.");
        }

        [Fact]
        public void CreateProject_WithStartDateAfterEndDate_ShouldThrowDomainException()
        {
            Action action = () => new Project("Proj", "desc", DateTime.Today.AddDays(5), DateTime.Today);
            action.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Start date must be before end date.");
        }

        #endregion
    }
}
