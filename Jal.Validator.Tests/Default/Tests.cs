using Jal.Factory.Interface;
using Jal.Locator.Impl;
using Jal.Validator.Impl;
using Jal.Validator.Interface;
using Jal.Validator.Tests.Impl;
using Jal.Validator.Tests.Model;
using NUnit.Framework;

namespace Jal.Validator.Tests.Default
{
    [TestFixture]
    public class Tests
    {
        private IModelValidator _modelValidator;

        [SetUp]
        public void SetUp()
        {
            var locator = new ServiceLocator();

            locator.Register(typeof(IValidator<Customer>), new CustomerValidator(), typeof(CustomerValidator).FullName);

            _modelValidator = ModelValidator.Create(new IObjectFactoryConfigurationSource[] { new AutoValidationConfigurationSource() }, locator);
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

