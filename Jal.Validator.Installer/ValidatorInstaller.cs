using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.Factory.Interface;
using Jal.Validator.Attributes;
using Jal.Validator.Impl;
using Jal.Validator.Interface;

namespace Jal.Validator.Installer
{
    public class ValidatorInstaller : IWindsorInstaller
    {
        private readonly Assembly[] _sourceassemblies;

        private readonly Assembly[] _configurationsourceassemblies;

        private readonly AbstractValidationConfigurationSource[] _sources;

        public ValidatorInstaller(Assembly[] sourceassemblies, Assembly[] configurationsourceassemblies)
        {
            _sourceassemblies = sourceassemblies;

            _configurationsourceassemblies = configurationsourceassemblies;
        }

        public ValidatorInstaller(AbstractValidationConfigurationSource[] sources)
        {
            _sources = sources;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblies = _sourceassemblies;
           
            if (assemblies != null)
            {
                foreach (var assembly in assemblies)
                {
                    var types = (assembly.GetTypes().Where(type =>
                                                           {
                                                               var isTransient = type.GetCustomAttributes(false).OfType<IsTransientAttribute>().Any();
                                                               return isTransient && typeof(IValidator).IsAssignableFrom(type);
                    }));
                    foreach (var t in types)
                    {
                        var service = t.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition()==typeof(IValidator<>));
                        if (service != null)
                        {
                            container.Register((Component.For(service).ImplementedBy(t).LifestyleTransient().Named(t.FullName)));
                        }
                    }

                    types = (assembly.GetTypes().Where(type => !type.GetCustomAttributes(false).OfType<IsTransientAttribute>().Any() && typeof(IValidator).IsAssignableFrom(type)));
                    foreach (var t in types)
                    {
                        var service = t.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>));
                        if (service != null)
                        {
                            container.Register((Component.For(service).ImplementedBy(t).LifestyleSingleton().Named(t.FullName)));
                        }
                    }
                }
            }

            container.Register(Component.For(typeof(IModelValidator)).ImplementedBy(typeof(ModelValidator)).LifestyleSingleton());

            container.Register(Component.For(typeof(IValidatorFactory)).ImplementedBy(typeof(ValidatorFactory)).LifestyleSingleton());

            var assembliessource = _configurationsourceassemblies;
            if (assembliessource != null)
            {
                foreach (var assembly in assembliessource)
                {
                    var assemblyDescriptor = Classes.FromAssembly(assembly);
                    container.Register(assemblyDescriptor.BasedOn<AbstractValidationConfigurationSource>().WithServiceAllInterfaces());
                }
            }

            if (_sources != null)
            {
                foreach (var source in _sources)
                {
                    container.Register(Component.For(typeof(IObjectFactoryConfigurationSource)).ImplementedBy(source.GetType()).Named(source.GetType().FullName).LifestyleSingleton());
                }
            }
        }
    }
}
