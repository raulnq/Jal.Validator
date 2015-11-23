using System;
using Jal.Validator.Impl;
using Jal.Validator.Tests.Attribute;
using Jal.Validator.Tests.Model;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;

namespace Jal.Validator.Tests
{
    [TestFixture]
    public class ModelValidatorTests
    {


        [Test]
        [ModelValidatorBuilder(true, true)]
        public void Validate_WithoutValidators_ShouldThrowException(ModelValidator sut)
        {
            Should.Throw<Exception>(() => { sut.Validate(new Customer()); });
        }

        [Test]
        [ModelValidatorBuilder]
        public void Validate_ShouldBeValid(ModelValidator sut)
        {
            var result = sut.Validate(new Customer());

            result.IsValid.ShouldBe(true);
        }

        [Test]
        [ModelValidatorBuilder(false)]
        public void Validate_ShouldBeInValid(ModelValidator sut)
        {
            var result = sut.Validate(new Customer());

            result.IsValid.ShouldBe(false);

            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        [ModelValidatorBuilder]
        public void Validate_WithRuleName_ShouldBeValid(ModelValidator sut)
        {
            var rulename = new Fixture().Create<string>();

            var result = sut.Validate(new Customer(), rulename);

            result.IsValid.ShouldBe(true);
        }


        [Test]
        [ModelValidatorBuilder]
        public void Validate_WithRuleNameAndRuleSet_ShouldBeValid(ModelValidator sut)
        {
            var fixture = new Fixture();

            var rulename = fixture.Create<string>();

            var ruleset = fixture.Create<string>();

            var result = sut.Validate(new Customer(), rulename, ruleset);

            result.IsValid.ShouldBe(true);
        }

        [Test]
        [ModelValidatorBuilder]
        public void Validate_WithContext_ShouldBeValid(ModelValidator sut)
        {
            var result = sut.Validate(new Customer(), new { });

            result.IsValid.ShouldBe(true);
        }

        [Test]
        [ModelValidatorBuilder(false)]
        public void Validate_WithContext_ShouldBeNotValid(ModelValidator sut)
        {
            var result = sut.Validate(new Customer(), new { });

            result.IsValid.ShouldBe(false);

            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        [ModelValidatorBuilder]
        public void Validate_WithRuleNameRuleSetAndContext_ShouldBeValid(ModelValidator sut)
        {
            var fixture = new Fixture();

            var rulename = fixture.Create<string>();

            var ruleset = fixture.Create<string>();

            var result = sut.Validate(new Customer(), rulename, ruleset, new { });

            result.IsValid.ShouldBe(true);
        }

        [Test]
        [ModelValidatorBuilder(false)]
        public void Validate_WithRuleNameRuleSetAndContext_ShouldNotBeValid(ModelValidator sut)
        {
            var fixture = new Fixture();

            var rulename = fixture.Create<string>();

            var ruleset = fixture.Create<string>();

            var result = sut.Validate(new Customer(), rulename, ruleset, new { });

            result.IsValid.ShouldBe(false);

            result.Errors.Count.ShouldBe(1);
        }
    }
}

