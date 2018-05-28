# Jal.Validator
Just another library to validate classes

## How to use?

### Default service locator

Create an instance of the locator

    var locator = new ServiceLocator();

Create your validator class

	public class CustomerValidator : AbstractValidator<Customer>
	{
		public override ValidationResult Validate(Customer instance)
		{
		    var result=new ValidationResult();
		
		    if (string.IsNullOrWhiteSpace(instance.Name))
		    {
		        result.Errors.Add(new ValidationFailure("Name", "The Name should not be empty"));
		    }
		
		    if (instance.Age<18)
		    {
		        result.Errors.Add(new ValidationFailure("Age", "The Age should be greater than 18"));
		    }
		
		    return result;
		}
	}

Create a class to setup the Jal.Validator library

    public class ValidationConfigurationSource : AbstractValidationConfigurationSource
    {
        public ValidationConfigurationSource()
        {
            Validate<Customer>().With<CustomerValidator>();
        }
    }

Register your validator

    locator.Register(typeof(IValidator<Customer>), new CustomerValidator(), typeof(CustomerValidator).FullName);
    
Create a instance of the IModelValidator interface

    ModelValidator.Create(new IObjectFactoryConfigurationSource[] { new AutoValidationConfigurationSource() }, locator)
    
Use the Validator class

	var customer = new Customer
	{
		Name = name,
		Age = age
	};
	var validationResult = validator.Validate(customer, "Group");

### Castle Windsor as service locator

Note: The [Jal.Locator.CastleWindsor](https://www.nuget.org/packages/Jal.Locator.CastleWindsor/) library is needed.

Setup the IoC container

	var container = new WindsorContainer();

	container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

Install the Jal.Locator.CastleWindsor library

	container.Install(new ServiceLocatorInstaller());

Install the Jal.Factory library

	container.Install(new FactoryInstaller(new IObjectFactoryConfigurationSource[]{}));
	
Install the Jal.Validator library

	container.Install(new ValidatorInstaller(new AbstractValidationConfigurationSource[]{new ValidationConfigurationSource()}));

Create your validator class

	public class CustomerValidator : AbstractValidator<Customer>
    {
        public override ValidationResult Validate(Customer instance)
        {
            var result=new ValidationResult();

            if (string.IsNullOrWhiteSpace(instance.Name))
            {
                result.Errors.Add(new ValidationFailure("Name", "The Name should not be empty"));
            }

            if (instance.Age<18)
            {
                result.Errors.Add(new ValidationFailure("Age", "The Age should be greater than 18"));
            }

            return result;
        }
    }

Create a class to setup the Jal.Validator library

    public class ValidationConfigurationSource : AbstractValidationConfigurationSource
    {
        public ValidationConfigurationSource()
        {
            Validate<Customer>().With<CustomerValidator>();
        }
    }

Register your validator class in the IoC container
	
    container.Register(Component.For(typeof(IValidator<Customer>)).ImplementedBy(typeof(CustomerValidator)).Named(typeof(CustomerValidator).FullName).LifestyleSingleton());

Resolve a instance of the interface IModelValidator

	var modelValidator = container.Resolve<IModelValidator>();

Use the Validator class

	 var customer = new Customer
            {
                Name = name,
                Age = age
            };
    var validationResult = modelValidator.Validate(customer);

### LightInject as service locator

Note: The [Jal.Locator.LightInject](https://www.nuget.org/packages/Jal.Locator.LightInject/) library is needed. 

Setup the IoC container

	var container = new ServiceContainer();

Install the Jal.Locator.CastleWindsor library

	container.RegisterFrom<ServiceLocatorCompositionRoot>();

Install the Jal.Factory library

	container.RegisterFactory(new IObjectFactoryConfigurationSource[] { });
	
Install the Jal.Validator library

	container.RegisterValidator(new AbstractValidationConfigurationSource[] { new ValidationConfigurationSource() });

Create your validator class

	public class CustomerValidator : AbstractValidator<Customer>
    {
        public override ValidationResult Validate(Customer instance)
        {
            var result=new ValidationResult();

            if (string.IsNullOrWhiteSpace(instance.Name))
            {
                result.Errors.Add(new ValidationFailure("Name", "The Name should not be empty"));
            }

            if (instance.Age<18)
            {
                result.Errors.Add(new ValidationFailure("Age", "The Age should be greater than 18"));
            }

            return result;
        }
    }

Create a class to setup the Jal.Validator library

    public class ValidationConfigurationSource : AbstractValidationConfigurationSource
    {
        public ValidationConfigurationSource()
        {
            Validate<Customer>().With<CustomerValidator>();
        }
    }
	
Register your validator class in the IoC container
	
    container.Register<IValidator<Customer>, CustomerValidator>(typeof(CustomerValidator).FullName, new PerContainerLifetime());
	
Resolve a instance of the interface IModelValidator

	var modelValidator = container.GetInstance<IModelValidator>();

Use the Validator class

	 var customer = new Customer
            {
                Name = name,
                Age = age
            };

    var validationResult = modelValidator.Validate(customer);
	
## FluentValidation Integration

Note: The Jal.Validator.FluentValidation library is needed

Create your validator class inheriting from the following class: Jal.Validator.FluentValidation.AbstractValidator

    public class CustomerValidator : Jal.Validator.FluentValidation.AbstractValidator<Customer>
    {
        public MandatoryFieldsValidator()
        {
            RuleFor(customer => customer.Name).NotNull();
        }
    }