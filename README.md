# Jal.Validator
Just another library to validate classes

## How to use?

### Default implementation

I only suggest to use this implementation on simple apps.

Create an instance of the locator

    var locator = ServiceLocator.Builder.Create as ServiceLocator;

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

    var factory = ObjectFactory.Builder.UseLocator(locator).UseConfigurationSource(new IObjectFactoryConfigurationSource[]{new ValidationConfigurationSource()}).Create;
    
    var validator = ModelValidator.Builder.UseFactory(factory).Create;
    
Use the Validator class

	var customer = new Customer
	{
		Name = name,
		Age = age
	};
	var validationResult = validator.Validate(customer, "Group");

### Castle Windsor Integration

Note: The Jal.Locator.CastleWindsor, Jal.Factory and Jal.Finder library are needed

Setup the Jal.Finder library

	var directory = AppDomain.CurrentDomain.BaseDirectory;

	var finder = AssemblyFinder.Builder.UsePath(directory).Create;

    var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();
	
Setup the Castle Windsor container

	var container = new WindsorContainer();

	container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

Install the Jal.Locator.CastleWindsor library

	container.Install(new ServiceLocatorInstaller());

Install the Jal.Factory library

	container.Install(new FactoryInstaller(assemblies));
	
Install the Jal.Validator library, use the ValidatorInstaller class included

	container.Install(new ValidatorInstaller(assemblies, assemblies));

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
	
Tag the assembly container of the validator classes in order to be read by the library

	[assembly: AssemblyTag]

Tag the assembly container of the validator configuration source classes in order to be read by the library

    [assembly: AssemblyTag]
	
Resolve a instance of the interface IModelValidator

	var modelValidator = container.Resolve<IModelValidator>();

Use the Validator class

	 var customer = new Customer
            {
                Name = name,
                Age = age
            };
    var validationResult = modelValidator.Validate(customer);

### LightInject Integration

Note: The Jal.Locator.LightInject, Jal.Factory and Jal.Finder library are needed

Setup the Jal.Finder library

	var directory = AppDomain.CurrentDomain.BaseDirectory;

	var finder = AssemblyFinder.Builder.UsePath(directory).Create;

    var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();
	
Setup the LightInject container

	var container = new ServiceContainer();

Install the Jal.Locator.CastleWindsor library

	container.RegisterFrom<ServiceLocatorCompositionRoot>();

Install the Jal.Factory library

	container.RegisterFactory(assemblies);
	
Install the Jal.Validator library, use the ValidatorInstaller class included

	container.RegisterValidator(assemblies, assemblies);

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
	
Tag the assembly container of the validator classes in order to be read by the library

	[assembly: AssemblyTag]

Tag the assembly container of the validator configuration source classes in order to be read by the library

    [assembly: AssemblyTag]
	
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