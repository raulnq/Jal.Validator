using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Jal.Factory.Installer;
using Jal.Factory.Interface;
using Jal.Finder.Atrribute;
using Jal.Finder.Impl;
using Jal.Locator.CastleWindsor.Installer;
using Jal.Validator.Impl;
using Jal.Validator.Installer;
using Jal.Validator.Interface;
using Jal.Validator.Tests.Impl;
using Jal.Validator.Tests.Model;
using NUnit.Framework;

namespace Jal.Validator.Tests.CastleWindsor
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Validate_WithoutRuleName_Valid(string name, int age)
        {
            var directory = TestContext.CurrentContext.TestDirectory;

            var finder = AssemblyFinder.Create(directory);

            var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

            IWindsorContainer container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(new ValidatorInstaller(assemblies, assemblies));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller(assemblies));

            var modelValidator = container.Resolve<IModelValidator>();

            var customer = new Customer
            {
                Name = name,
                Age = age
            };
            var validationResult = modelValidator.Validate(customer);
            Assert.AreEqual(true, validationResult.IsValid);
        }

        [Test]
        [TestCase("", 15)]
        [TestCase(null, 1)]
        [TestCase(" ", -1)]
        [TestCase("  ", 0)]
        public void Validate_WithoutRuleName_IsNotValid(string name, int age)
        {
            var directory = TestContext.CurrentContext.TestDirectory;

            var finder = AssemblyFinder.Create(directory);

            var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

            IWindsorContainer container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(new ValidatorInstaller(assemblies, assemblies));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller(assemblies));

            var modelValidator = container.Resolve<IModelValidator>();

            var customer = new Customer
            {
                Name = name,
                Age = age
            };
            var validationResult = modelValidator.Validate(customer);
            Assert.AreEqual(false, validationResult.IsValid);
            Assert.AreEqual(2, validationResult.Errors.Count);
        }

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Validate_WithRuleName_IsValid(string name, int age)
        {
            IWindsorContainer container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(new ValidatorInstaller(new AbstractValidationConfigurationSource[]{new AutoValidationConfigurationSource()}));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller(new IObjectFactoryConfigurationSource[]{}));

            container.Register(Component.For(typeof(IValidator<Customer>)).ImplementedBy(typeof(CustomerValidator)).Named(typeof(CustomerValidator).FullName).LifestyleSingleton());

            var modelValidator = container.Resolve<IModelValidator>();

            var customer = new Customer
            {
                Name = name,
                Age = age
            };
            var validationResult = modelValidator.Validate(customer, "Group");

            Assert.AreEqual(true, validationResult.IsValid);
        }
    }
}

