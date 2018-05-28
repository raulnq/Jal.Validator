using System.Linq;
using System.Reflection;
using Jal.Factory.Interface;
using Jal.Validator.Attributes;
using Jal.Validator.Impl;
using Jal.Validator.Interface;
using LightInject;

namespace Jal.Validator.LightInject.Installer
{
    public static class ServiceContainerExtension
    {
        public static void RegisterValidator(this IServiceContainer container, Assembly[] validatorSourceAssemblies, Assembly[] validationConfigurationSourceAssemblies)
        {
            container.Register<IModelValidator, ModelValidator>(new PerContainerLifetime());

            container.Register<IValidatorFactory, ValidatorFactory>(new PerContainerLifetime());

            var assemblysources = validationConfigurationSourceAssemblies;

            if (assemblysources != null)
            {
                foreach (var assemblysource in assemblysources)
                {
                    foreach (var exportedType in assemblysource.ExportedTypes)
                    {
                        if (exportedType.IsSubclassOf(typeof(AbstractValidationConfigurationSource)))
                        {
                            container.Register(typeof(AbstractValidationConfigurationSource), exportedType, exportedType.FullName, new PerContainerLifetime());
                        }
                    }
                }
            }

            if(validatorSourceAssemblies!=null)
            { 
                foreach (var assembly in validatorSourceAssemblies)
                {
                    var types = (assembly.GetTypes().Where(type =>
                    {
                        var isTransient = type.GetCustomAttributes(false).OfType<IsTransientAttribute>().Any();

                        return isTransient && typeof(IValidator).IsAssignableFrom(type);
                    }));

                    foreach (var t in types)
                    {
                        var service = t.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>));
                        if (service != null)
                        {
                            container.Register(service, t, t.FullName);
                        }
                    }

                    types = (assembly.GetTypes().Where(type => !type.GetCustomAttributes(false).OfType<IsTransientAttribute>().Any() && typeof(IValidator).IsAssignableFrom(type)));

                    foreach (var t in types)
                    {
                        var service = t.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>));
                        if (service != null)
                        {
                            container.Register(service, t, t.FullName, new PerContainerLifetime());
                        }
                    }
                }
            }
        }

        public static void RegisterValidator(this IServiceContainer container, AbstractValidationConfigurationSource[] sources)
        {
            container.Register<IModelValidator, ModelValidator>(new PerContainerLifetime());

            container.Register<IValidatorFactory, ValidatorFactory>(new PerContainerLifetime());

            if (sources != null)
            {
                foreach (var source in sources)
                {
                    container.Register(typeof(IObjectFactoryConfigurationSource), source.GetType(), source.GetType().FullName, new PerContainerLifetime());
                }
            }         
        }
    }
}
