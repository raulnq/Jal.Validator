using Jal.Factory.LightInject.Installer;
using Jal.Finder.Atrribute;
using Jal.Finder.Impl;
using Jal.Locator.LightInject.Installer;
using Jal.Validator.Interface;
using Jal.Validator.LightInject.Installer;
using Jal.Validator.Tests.Model;
using LightInject;
using NUnit.Framework;

namespace Jal.Validator.Tests.Lightinject
{
    [TestFixture]
    public class Tests
    {
        private IModelValidator _modelValidator;

        [SetUp]
        public void SetUp()
        {

            var container = new ServiceContainer();

            var directory = TestContext.CurrentContext.TestDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            container.RegisterFactory(assemblies);

            container.RegisterValidator(assemblies, assemblies);

            _modelValidator = container.GetInstance<IModelValidator>();
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
    }
}

