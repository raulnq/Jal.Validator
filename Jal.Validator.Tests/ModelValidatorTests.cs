using System;
using Jal.Validator.Extension;
using Jal.Validator.Impl;
using Jal.Validator.Interface;
using Jal.Validator.Model;
using Jal.Validator.Tests.Impl;
using Jal.Validator.Tests.Model;
using Moq;
using NUnit.Framework;
using Shouldly;
using ValidationResult = Jal.Validator.Model.ValidationResult;

namespace Jal.Validator.Tests
{
    [TestFixture]
    public class ModelValidatorTests
    {


        [Test]
        public void Validate_WithNoValidators_ShouldBeTrue()
        {
            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x => x.Create(It.IsAny<Customer>(), It.IsAny<string>())).Throws<Exception>();

            var sut = new ModelValidator(factory.Object);

            Should.Throw<Exception>(() => { var result = sut.Validate(new Customer()); });

        }

        [Test]
        public void Validate_WithNullValidators_ShouldBeTrue()
        {
            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x => x.Create(It.IsAny<Customer>(), It.IsAny<string>())).Returns((IValidator<Customer>[])null);

            var sut = new ModelValidator(factory.Object);

            var result = sut.Validate(new Customer());

            result.ShouldNotBeNull();

            result.IsValid.ShouldBeTrue();
        }

        [Test]
        public void Validate_With_ShouldBeTrue()
        {
            var validator = new Mock<IValidator<Customer>>();

            validator.Setup(x => x.Validate(It.IsAny<Customer>())).Returns(new ValidationResult());

            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x => x.Create(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new IValidator<Customer>[] { validator.Object });

            var sut = new ModelValidator(factory.Object);

            var result = sut.Validate(new Customer());

            result.IsValid.ShouldBeTrue();
        }

        [Test]
        public void Validate_With_ShouldBeFalse()
        {
            var validator = new Mock<IValidator<Customer>>();

            validator.Setup(x => x.Validate(It.IsAny<Customer>())).Returns(new ValidationResult(new [] {new ValidationFailure(""), }));

            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x => x.Create(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new IValidator<Customer>[] { validator.Object });

            var sut = new ModelValidator(factory.Object);

            var result = sut.Validate(new Customer());

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Validate_WithRuleName_ShouldBeValid()
        {
            var validator = new Mock<IValidator<Customer>>();

            validator.Setup(x => x.Validate(It.IsAny<Customer>())).Returns(new ValidationResult());

            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x=>x.Create(It.IsAny<Customer>(),It.IsAny<string>())).Returns(new IValidator<Customer>[] { validator.Object});

            var sut = new ModelValidator(factory.Object);

            var result = sut.Validate(new Customer(), "rulename");

            result.IsValid.ShouldBeTrue();
        }


        [Test]
        public void Validate_WithRuleNameAndRuleSet_ShouldBeValid()
        {
            var validator = new Mock<IValidator<Customer>>();

            validator.Setup(x => x.Validate(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new ValidationResult());

            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x => x.Create(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new IValidator<Customer>[] { new CustomerValidator() });

            var sut = new ModelValidator(factory.Object);

            var result = sut.Validate(new Customer(), "rulename", "ruleset");

            result.IsValid.ShouldBe(true);
        }

        [Test]
        public void Validate_WithContext_ShouldBeTrue()
        {
            var validator = new Mock<IValidator<Customer>>();

            validator.Setup(x => x.Validate(It.IsAny<Customer>(), It.Is<object>(o => true))).Returns(new ValidationResult());

            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x => x.Create(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new IValidator<Customer>[] { validator.Object });

            var sut = new ModelValidator(factory.Object);

            var result = sut.Validate(new Customer(), new { });

            result.IsValid.ShouldBeTrue();
        }

        [Test]
        public void Validate_WithRuleNameRuleSetAndContext_ShouldBeValid()
        {
            var validator = new Mock<IValidator<Customer>>();

            validator.Setup(x => x.Validate(It.IsAny<Customer>(),It.IsAny<string>(), It.Is<object>(o => true))).Returns(new ValidationResult());

            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x => x.Create(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new IValidator<Customer>[] { validator.Object });

            var sut = new ModelValidator(factory.Object);

            var result = sut.Validate(new Customer(), "rulename", "ruleset", new { });

            result.IsValid.ShouldBe(true);
        }
        [Test]
        public void Validate_WithRuleNameAndContext_ShouldBeValid()
        {
            var validator = new Mock<IValidator<Customer>>();

            validator.Setup(x => x.Validate(It.IsAny<Customer>(), It.Is<object>(o => true))).Returns(new ValidationResult());

            var factory = new Mock<IValidatorFactory>();

            factory.Setup(x => x.Create(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new IValidator<Customer>[] { validator.Object });

            var sut = new ModelValidator(factory.Object);

            var result = sut.Validate(new Customer(), "rulename", new { });

            result.IsValid.ShouldBe(true);
        }
    }

    public class StringExtensionTests
    {
        [Test]
        public void ToValidationResult_With_ShouldBeFalse()
        {
            var result = "".ToValidationResult("property","code","info");

            result.ShouldNotBeNull();

            result.IsValid.ShouldBeFalse();
        }

        [Test]
        public void ToValidationFailure_With_ShouldBeFalse()
        {
            var result = "".ToValidationFailure("property", "code", "info");

            result.ShouldNotBeNull();

            result.ShouldBeOfType<ValidationFailure>();
        }
    }

    public class ValidatorExtensionTests
    {
        [Test]
        public void IsValid_With_ShouldNotBeNull()
        {
            var sut = new CustomerValidator();

            var result = sut.IsValid(new Customer());

            result.ShouldNotBeNull();
        }

        [Test]
        public void IsValid_WithContext_ShouldNotBeNull()
        {
            var sut = new CustomerValidator();

            var result = sut.IsValid(new Customer(), new {});

            result.ShouldNotBeNull();
        }

        [Test]
        public void ShouldHaveValidationErrorFor_With_ShouldNotThrowException()
        {
            var sut = new CustomerValidator();

            sut.ShouldHaveValidationErrorFor(new Customer());
        }

        [Test]
        public void ShouldHaveValidationErrorFor_With_ShouldThrowException()
        {
            var sut = new CustomerValidator();

            Should.Throw<ValidationException>(()=> { sut.ShouldHaveValidationErrorFor(new Customer() { Name = "Name", Age = 20}); }) ;
        }

        [Test]
        public void ShouldHaveValidationErrorFor_WithContext_ShouldNotThrowException()
        {
            var sut = new CustomerValidator();

            sut.ShouldHaveValidationErrorFor(new Customer(),new {});
        }

        [Test]
        public void ShouldHaveValidationErrorFor_WithContext_ShouldThrowException()
        {
            var sut = new CustomerValidator();

            Should.Throw<ValidationException>(() => { sut.ShouldHaveValidationErrorFor(new Customer() { Name = "Name", Age = 20 }, new {}); });
        }

        [Test]
        public void ShouldHaveValidationErrorFor_WithRuleSet_ShouldNotThrowException()
        {
            var sut = new CustomerValidator();

            sut.ShouldHaveValidationErrorFor(new Customer(), "GreaterThan21");
        }

        [Test]
        public void ShouldHaveValidationErrorFor_WithRuleSet_ShouldThrowException()
        {
            var sut = new CustomerValidator();

            Should.Throw<ValidationException>(() => { sut.ShouldHaveValidationErrorFor(new Customer() { Name = "Name", Age = 22 }, "GreaterThan21"); });
        }

        [Test]
        public void ShouldHaveValidationErrorFor_WithRuleSetAnContext_ShouldNotThrowException()
        {
            var sut = new CustomerValidator();

            sut.ShouldHaveValidationErrorFor(new Customer(), "GreaterThan21", new {});
        }

        [Test]
        public void ShouldHaveValidationErrorFor_WithRuleSetAndContext_ShouldThrowException()
        {
            var sut = new CustomerValidator();

            Should.Throw<ValidationException>(() => { sut.ShouldHaveValidationErrorFor(new Customer() { Name = "Name", Age = 22 }, "GreaterThan21", new {}); });
        }

        [Test]
        public void IsValid_WithRuleSet_ShouldNotBeNull()
        {
            var sut = new CustomerValidator();

            var result = sut.IsValid(new Customer(), "ruleset");

            result.ShouldNotBeNull();
        }

        [Test]
        public void IsValid_WithRuleSetAndContext_ShouldNotBeNull()
        {
            var sut = new CustomerValidator();

            var result = sut.IsValid(new Customer(), "ruleset", new {});

            result.ShouldNotBeNull();
        }

        [Test]
        public void ShouldNotHaveValidationErrorFor_With_ShouldThrowException()
        {
            var sut = new CustomerValidator();

            Should.Throw<ValidationException>(() => { sut.ShouldNotHaveValidationErrorFor(new Customer() ); });
        }

        [Test]
        public void ShouldNotHaveValidationErrorFor_With_ShouldNotThrowException()
        {
            var sut = new CustomerValidator();

            sut.ShouldNotHaveValidationErrorFor(new Customer() { Name = "Name", Age = 20 });
        }

        [Test]
        public void ShouldNotHaveValidationErrorFor_WithContext_ShouldThrowException()
        {
            var sut = new CustomerValidator();

            Should.Throw<ValidationException>(() => { sut.ShouldNotHaveValidationErrorFor(new Customer(), new { }); });
        }

        [Test]
        public void ShouldNotHaveValidationErrorFor_WithContext_ShouldNotThrowException()
        {
            var sut = new CustomerValidator();

            sut.ShouldNotHaveValidationErrorFor(new Customer() { Name = "Name", Age = 20 }, new { }); 
        }

        [Test]
        public void ShouldNotHaveValidationErrorFor_WithRuleSet_ShouldThrowException()
        {
            var sut = new CustomerValidator();

            Should.Throw<ValidationException>(() => { sut.ShouldNotHaveValidationErrorFor(new Customer(), "GreaterThan21"); });
        }

        [Test]
        public void ShouldNotHaveValidationErrorFor_WithRuleSet_ShouldNotThrowException()
        {
            var sut = new CustomerValidator();

            sut.ShouldNotHaveValidationErrorFor(new Customer() { Name = "Name", Age = 22 }, "GreaterThan21"); 
        }

        [Test]
        public void ShouldNotHaveValidationErrorFor_WithRuleSetAnContext_ShouldThrowException()
        {
            var sut = new CustomerValidator();

            Should.Throw<ValidationException>(() => { sut.ShouldNotHaveValidationErrorFor(new Customer(), "GreaterThan21", new { }); });
        }

        [Test]
        public void ShouldNotHaveValidationErrorFor_WithRuleSetAndContext_ShouldNotThrowException()
        {
            var sut = new CustomerValidator();

            sut.ShouldNotHaveValidationErrorFor(new Customer() { Name = "Name", Age = 22 }, "GreaterThan21", new { }); 
        }
    }
}

