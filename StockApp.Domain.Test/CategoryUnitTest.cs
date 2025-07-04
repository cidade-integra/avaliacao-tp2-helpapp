﻿using StockApp.Domain.Entities;
using StockApp.Domain.Validation;
using FluentAssertions;

namespace StockApp.Domain.Test
{
    public class CategoryUnitTest
    {
        #region Testes Positivos

        [Fact(DisplayName = "Create Category With Valid State Alone Name")]
        public void CreateCategory_WithAloneName_ResultObjectValidState()
        {
            Action action = () => new Category("Category Name");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should().NotThrow<DomainExceptionValidation>();
        }


        #endregion

        #region Testes Negativos

        [Fact(DisplayName = "Create Category With Name Empty")]
        public void CreateCategory_WithNameEmpty_ResultObjectException()
        {
            Action action = () => new Category(1, "");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, name is required.");
        }

        [Fact(DisplayName = "Create Category With Name Null")]
        public void CreateCategory_WithNameNull_ResultObjectException()
        {
            Action action = () => new Category(1, null);
            action.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, name is required.");
        }

        [Fact(DisplayName = "Create Category With Too Short")]
        public void CreateCategory_WithTooShort_ResultObjectException()
        {
            Action action = () => new Category(1, "Ca");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, too short, minimum 3 characters.");
        }

        [Fact(DisplayName = "Create Category With Id Invalid")]
        public void CreateCategory_WithIdInvalid_ResultObjectException()
        {
            Action action = () => new Category(-1, "");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value.");
        }        

        #endregion
    }
}
