using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Jal.Factory.Impl;
using Jal.Factory.Installer;
using Jal.Factory.Interface;
using Jal.Locator.CastleWindsor.Installer;
using Jal.Locator.Impl;
using Jal.Validator.Impl;
using Jal.Validator.Installer;
using Jal.Validator.Interface;
using Jal.Validator.Tests.Impl;
using Jal.Validator.Tests.Model;
using NUnit.Framework;

namespace Jal.Validator.Tests.Integration
{
    [TestFixture]
    public class Tests
    {
        private IModelValidator _modelValidator;

        [SetUp]
        public void SetUp()
        {
            var finder = AssemblyFinder.Impl.AssemblyFinder.Builder.UsePath(TestContext.CurrentContext.TestDirectory).Create;

            IWindsorContainer container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(new ValidatorInstaller(finder.GetAssemblies("Validator"), finder.GetAssemblies("ValidatorSource")));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller(finder.GetAssemblies("FactorySource")));

            _modelValidator = container.Resolve<IModelValidator>();
        }

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Validate_WithoutRuleName_Valid(string name, int age)
        {
            var customer = new Customer
            {
                Name = name,
                Age = age
            };
            var validationResult = _modelValidator.Validate(customer);
            Assert.AreEqual(true, validationResult.IsValid);
        }

        [Test]
        [TestCase("", 15)]
        [TestCase(null, 1)]
        [TestCase(" ", -1)]
        [TestCase("  ", 0)]
        public void Validate_WithoutRuleName_IsNotValid(string name, int age)
        {
            var customer = new Customer
            {
                Name = name,
                Age = age
            };
            var validationResult = _modelValidator.Validate(customer);
            Assert.AreEqual(false, validationResult.IsValid);
            Assert.AreEqual(2, validationResult.Errors.Count);
        }

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Validate_WithRuleName_IsValid(string name, int age)
        {
            var customer = new Customer
            {
                Name = name,
                Age = age
            };
            var validationResult = _modelValidator.Validate(customer, "Group");
            Assert.AreEqual(true, validationResult.IsValid);
        }

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Validate_WithRuleName_IsValid1(string name, int age)
        {
            var locator = ServiceLocator.Builder.Create as ServiceLocator;

            locator.Register(typeof(IValidator<Customer>), new CustomerValidator(), typeof(CustomerValidator).FullName);

            var factory = ObjectFactory.Builder.UseLocator(locator).UseConfigurationSource(new IObjectFactoryConfigurationSource[]{new ValidationConfigurationSource()}).Create;

            var validator = ModelValidator.Builder.UseFactory(factory).Create;

            var customer = new Customer
            {
                Name = name,
                Age = age
            };
            var validationResult = validator.Validate(customer, "Group");

            Assert.AreEqual(true, validationResult.IsValid);
        }
    }
}

