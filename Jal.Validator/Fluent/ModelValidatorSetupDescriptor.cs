using System;
using Jal.Validator.Impl;
using Jal.Validator.Interface;

namespace Jal.Validator.Fluent
{
    public class ModelValidatorSetupDescriptor : IModelValidatorSetupDescriptor, IValidatorFactorySetupDescriptor
    {
        private IValidatorFactory _validatorFactory;

        private IModelValidator _modelValidator;

        public IModelValidatorSetupDescriptor UseValidatorFactory(IValidatorFactory validatorFactory)
        {
            if (validatorFactory == null)
            {
                throw new ArgumentNullException("validatorFactory");
            }

            _validatorFactory = validatorFactory;

            return this;
        }

        public IModelValidatorSetupDescriptor UseModelValidator(IModelValidator modelValidator)
        {
            _modelValidator = modelValidator;
            return this;
        }

        public IModelValidator Create()
        {
            if (_modelValidator != null)
            {
                return _modelValidator;
            }
            else
            {
                return new ModelValidator(_validatorFactory);
            }
        }
    }
}