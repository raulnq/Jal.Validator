using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.Validator.Impl;
using Jal.Validator.Interface;

namespace Jal.Validator.Installer
{
    public class ValidatorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblies = AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("Validator");
           
            if (assemblies != null)
            {
                foreach (var assembly in assemblies)
                {
                    var types = (assembly.GetTypes().Where(type => typeof(IValidatorContextContainer).IsAssignableFrom(type) && typeof(IValidator).IsAssignableFrom(type)));
                    foreach (var t in types)
                    {
                        var service = t.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition()==typeof(IValidator<>));
                        if (service != null)
                        {
                            container.Register((Component.For(service).ImplementedBy(t).LifestyleTransient().Named(t.FullName)));
                        }
                    }

                    types = (assembly.GetTypes().Where(type => !typeof(IValidatorContextContainer).IsAssignableFrom(type) && typeof(IValidator).IsAssignableFrom(type)));
                    foreach (var t in types)
                    {
                        var service = t.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>));
                        if (service != null)
                        {
                            container.Register((Component.For(service).ImplementedBy(t).LifestyleSingleton().Named(t.FullName)));
                        }
                    }
                }
                container.Register(Component.For(typeof(IModelValidator)).ImplementedBy(typeof(ModelValidator)).LifestyleSingleton());
            }
            else
            {
                throw new Exception("There is not a validator assembly");
            }

            var assemblysource = AssemblyFinder.Impl.AssemblyFinder.Current.GetAssembly("ValidatorSource");
            if (assemblysource != null)
            {
                var assemblyDescriptor = Classes.FromAssembly(assemblysource);
                container.Register(assemblyDescriptor.BasedOn<AbstractValidationConfigurationSource>().WithServiceAllInterfaces());
            }
        }
    }
}