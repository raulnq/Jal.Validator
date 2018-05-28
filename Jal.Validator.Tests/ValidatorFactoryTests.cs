using Jal.Factory.Interface;
using Jal.Validator.Impl;
using Jal.Validator.Interface;
using Jal.Validator.Tests.Model;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Jal.Validator.Tests
{
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
}