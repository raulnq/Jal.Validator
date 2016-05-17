using System;
using Jal.Factory.Interface;
using Jal.Validator.Impl;
using Jal.Validator.Interface;

namespace Jal.Validator.Fluent
{
    public class ModelValidatorSetupDescriptor : IModelValidatorSetupDescriptor, IModelValidatorObjectFactorySetupDescriptor
    {
        private IObjectFactory _objectFactory;

        private IModelValidator _modelValidator;

        public IModelValidatorSetupDescriptor UseObjectFactory(IObjectFactory objectFactory)
        {
            if (objectFactory == null)
            {
                throw new ArgumentNullException("objectFactory");
            }
            _objectFactory = objectFactory;
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
                return new ModelValidator(_objectFactory);
            }
        }
    }
}