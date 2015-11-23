# Jal.Validator
Just another library to validate classes

## How to use?

Note: The Jal.Locator.CastleWindsor, Jal.Factory and Jal.AssemblyFinder library are needed.

### Setup the Jal.AssemblyFinder library

	var directory = AppDomain.CurrentDomain.BaseDirectory;
	AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(directory);
	
### Setup the Castle Windsor container

	var container = new WindsorContainer();

### Install the Jal.Locator.CastleWindsor library

	container.Install(new ServiceLocatorInstaller());

### Install the Jal.Factory library

	container.Install(new FactoryInstaller());
	
### Install the Jal.Validator library, use the ValidatorInstaller class included

	container.Install(new ValidatorInstaller());

### Create your validator class

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

### Create a class to setup the Jal.Validator library 

    public class ValidationConfigurationSource : AbstractValidationConfigurationSource
    {
        public ValidationConfigurationSource()
        {
            Validate<Customer>().With<CustomerValidator>();
        }
    }
	
### Tag the assembly container of the validator classes in order to be read by the library

	[assembly: AssemblyTag("Validator")]

### Tag the assembly container of the validator configuration source classes in order to be read by the library

	[assembly: AssemblyTag("ValidatorSource")]
	
### Resolve a instance of the interface IModelValidator

	var modelValidator = container.Resolve<IModelValidator>();

### Use the Validator class

	 var customer = new Customer
            {
                Name = name,
                Age = age
            };
    var validationResult = modelValidator.Validate(customer);
	
## FluentValidation Integration

Note: The Jal.Validator.FluentValidation library is needed.

### Create your validator class inheriting from the following class: Jal.Validator.FluentValidation.AbstractValidator

