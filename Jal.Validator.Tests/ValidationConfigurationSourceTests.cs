using Jal.Validator.Tests.Impl;
using Jal.Validator.Tests.Model;
using NUnit.Framework;
using Shouldly;

namespace Jal.Validator.Tests
{
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

            configuration.Items[0].Name = "group";

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

            configuration.Items[0].Name = "Group";

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(CustomerValidator));

            configuration.Items[0].Selector.ShouldNotBeNull();
        }
    }
}