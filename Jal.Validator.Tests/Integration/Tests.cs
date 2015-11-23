using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Jal.Factory.Installer;
using Jal.Locator.CastleWindsor.Installer;
using Jal.Validator.Installer;
using Jal.Validator.Interface;
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
            AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(TestContext.CurrentContext.TestDirectory);
            IWindsorContainer container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
            container.Install(new ValidatorInstaller());
            container.Install(new ServiceLocatorInstaller());
            container.Install(new FactoryInstaller());
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
            var validationResult = _modelValidator.Validate(customer, "RuleName");
            Assert.AreEqual(true, validationResult.IsValid);
        }
    }
}

