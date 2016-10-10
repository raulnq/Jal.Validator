using System;
using Jal.Factory.Interface;
using Jal.Locator.Interface;
using Jal.Validator.Extension;
using Jal.Validator.Fluent.Impl;
using Jal.Validator.Fluent.Interface;
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

    public class ModelValidatorFluentBuilderTests
    {
        [Test]
        public void UseFactory_With_ShouldNotBeNull()
        {
            var sut = new ModelValidatorFluentBuilder();

            var factory = new Mock<IObjectFactory>();

            var chain = sut.UseFactory(factory.Object);

            chain.ShouldNotBeNull();

            sut.ValidatorFactory.ShouldNotBeNull();

            chain.ShouldBeAssignableTo<IModelValidatorFluentBuilder>();
        }

        [Test]
        public void UseFactory_WithNull_ShouldThrowException()
        {
            var sut = new ModelValidatorFluentBuilder();

            Should.Throw<ArgumentNullException>(() => { var chain = sut.UseFactory(null); });
        }

        [Test]
        public void UseInterceptor_With_ShouldNotBeNull()
        {
            var sut = new ModelValidatorFluentBuilder();

            var interceptor = new Mock<IModelValidatorInterceptor>();

            var chain = sut.UseInterceptor(interceptor.Object);

            chain.ShouldNotBeNull();

            sut.ModelValidatorInterceptor.ShouldNotBeNull();

            chain.ShouldBeAssignableTo<IModelValidatorEndFluentBuilder>();
        }

        [Test]
        public void UseInterceptor_WithNull_ShouldThrowException()
        {
            var sut = new ModelValidatorFluentBuilder();

            Should.Throw<ArgumentNullException>(() => { var chain = sut.UseInterceptor(null); });
        }

        [Test]
        public void Create_WithFactoryAndInterceptor_ShouldNotBeNull()
        {
            var sut = new ModelValidatorFluentBuilder();

            var interceptor = new Mock<IModelValidatorInterceptor>();

            var factory = new Mock<IObjectFactory>();

            var validation = sut.UseFactory(factory.Object).UseInterceptor(interceptor.Object).Create;

            validation.ShouldNotBeNull();

            validation.ShouldBeAssignableTo<IModelValidator>();
        }
    }
    public class ValidationConfigurationSourceTests
    {
        [Test]
        public void Source_WithValidate_ShouldNotBeNull()
        {
            var sut = new ValidationConfigurationSource();

            sut.Validate<Customer>();

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBeNull();

            configuration.Items[0].Selector.ShouldBeNull();
        }

        [Test]
        public void Source_WithValidateWith_ShouldNotBeNull()
        {
            var sut = new ValidationConfigurationSource();

            sut.Validate<Customer>().With<CustomerValidator>();

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(CustomerValidator));

            configuration.Items[0].Selector.ShouldBeNull();
        }

        [Test]
        public void Source_WithValidateWithWhen_ShouldNotBeNull()
        {
            var sut = new ValidationConfigurationSource();

            sut.Validate<Customer>().With<CustomerValidator>().When(x=>x.Age>10);

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(CustomerValidator));

            configuration.Items[0].Selector.ShouldNotBeNull();
        }

        [Test]
        public void Source_WithNamedValidateNullSetup_ShouldNotBeNull()
        {
            var sut = new ValidationConfigurationSource();

            sut.Validate<Customer>("group", null);

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.ShouldBeEmpty();
        }

        [Test]
        public void Source_WithNamedValidateNoSetup_ShouldNotBeNull()
        {
            var sut = new ValidationConfigurationSource();

            sut.Validate<Customer>("group", x=> {});

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.ShouldBeEmpty();
        }

        [Test]
        public void Source_WithNamedValidateCreate_ShouldBeOne()
        {
            var sut = new ValidationConfigurationSource();

            sut.Validate<Customer>("group", x => { x.With<CustomerValidator>(); });
            
            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].GroupName = "group";

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(CustomerValidator));

            configuration.Items[0].Selector.ShouldBeNull();
        }

        [Test]
        public void Source_WithNamedValidateCreateWhen_ShouldBeOne()
        {
            var sut = new ValidationConfigurationSource();

            sut.Validate<Customer>("group", x => { x.With<CustomerValidator>().When(y=>y.Age>10); });

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].GroupName = "Group";

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(CustomerValidator));

            configuration.Items[0].Selector.ShouldNotBeNull();
        }
    }
    public class ValidatorFactoryTests
    {
        [Test]
        public void Create_WithNoGroup_ShouldNotBeNull()
        {
            var factory = new Mock<IObjectFactory>();

            factory.Setup(x => x.Create<Customer,IValidator<Customer>>(It.IsAny<Customer>())).Returns(new IValidator<Customer>[] {});

            var sut = new ValidatorFactory(factory.Object);

            var validator = sut.Create(new Customer(), string.Empty);

            validator.ShouldNotBeNull();

        }

        [Test]
        public void Create_WithGroup_ShouldNotBeNull()
        {
            var factory = new Mock<IObjectFactory>();

            factory.Setup(x => x.Create<Customer, IValidator<Customer>>(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new IValidator<Customer>[] { });

            var sut = new ValidatorFactory(factory.Object);

            var validator = sut.Create(new Customer(), "group");

            validator.ShouldNotBeNull();

        }
    }

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

        [Test]
        public void Builder_With_ShouldNotBeNull()
        {
            var chain = ModelValidator.Builder;

            chain.ShouldNotBeNull();

            chain.ShouldBeAssignableTo<IModelValidatorStartFluentBuilder>();
        }
    }
}

