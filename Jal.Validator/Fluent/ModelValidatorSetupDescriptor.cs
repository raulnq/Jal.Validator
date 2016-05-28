using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Locator.Interface;
using Jal.Validator.Impl;
using Jal.Validator.Interface;
using Jal.Validator.Interface.Fluent;

namespace Jal.Validator.Fluent
{
    public class ModelValidatorSetupDescriptor : IModelValidatorSetupDescriptor, IModelValidatorStartSetupDescriptor
    {
        private IValidatorFactory _validatorFactory;

        private IModelValidator _modelValidator;

        private IModelValidatorInterceptor _modelValidatorInterceptor;

        public IModelValidatorSetupDescriptor UseObjectFactory(IObjectFactory objectFactory)
        {
            if (objectFactory == null)
            {
                throw new ArgumentNullException("objectFactory");
            }
            _validatorFactory = new ValidatorFactory(objectFactory);
            return this;
        }

        public IModelValidatorSetupDescriptor UseFactory(IValidatorFactory validatorFactory)
        {
            if (validatorFactory == null)
            {
                throw new ArgumentNullException("validatorFactory");
            }

            _validatorFactory = validatorFactory;

            return this;
        }

        public IModelValidatorEndSetupDescriptor UseModelValidator(IModelValidator modelValidator)
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
                IModelValidatorInterceptor modelValidatorInterceptor = new NullModelValidatorInterceptor();

                if (_modelValidatorInterceptor != null)
                {
                    modelValidatorInterceptor = _modelValidatorInterceptor;
                }

                return new ModelValidator(_validatorFactory, modelValidatorInterceptor);
            }
        }

        public IModelValidatorSetupDescriptor UseInterceptor(IModelValidatorInterceptor modelValidatorInterceptor)
        {
            _modelValidatorInterceptor = modelValidatorInterceptor;
            return this;
        }
    }
}