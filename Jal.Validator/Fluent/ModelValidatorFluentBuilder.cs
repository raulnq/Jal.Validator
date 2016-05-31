using System;
using Jal.Factory.Interface;
using Jal.Validator.Impl;
using Jal.Validator.Interface;
using Jal.Validator.Interface.Fluent;

namespace Jal.Validator.Fluent
{
    public class ModelValidatorFluentBuilder : IModelValidatorFluentBuilder, IModelValidatorStartFluentBuilder
    {
        private IValidatorFactory _validatorFactory;

        private IModelValidator _modelValidator;

        private IModelValidatorInterceptor _modelValidatorInterceptor;

        public IModelValidatorFluentBuilder UseObjectFactory(IObjectFactory objectFactory)
        {
            if (objectFactory == null)
            {
                throw new ArgumentNullException("objectFactory");
            }
            _validatorFactory = new ValidatorFactory(objectFactory);
            return this;
        }

        public IModelValidatorFluentBuilder UseFactory(IValidatorFactory validatorFactory)
        {
            if (validatorFactory == null)
            {
                throw new ArgumentNullException("validatorFactory");
            }

            _validatorFactory = validatorFactory;

            return this;
        }

        public IModelValidatorEndFluentBuilder UseModelValidator(IModelValidator modelValidator)
        {
            if (modelValidator == null)
            {
                throw new ArgumentNullException("modelValidator");
            }
            _modelValidator = modelValidator;
            return this;
        }

        public IModelValidator Create
        {
            get
            {
                if (_modelValidator != null)
                {
                    return _modelValidator;
                }
                else
                {

                    var result = new ModelValidator(_validatorFactory);

                    IModelValidatorInterceptor modelValidatorInterceptor = new NullModelValidatorInterceptor();

                    if (_modelValidatorInterceptor != null)
                    {
                        modelValidatorInterceptor = _modelValidatorInterceptor;
                    }

                    result.Interceptor = modelValidatorInterceptor;

                    return result;
                }
            }
        }

        public IModelValidatorFluentBuilder UseInterceptor(IModelValidatorInterceptor modelValidatorInterceptor)
        {
            _modelValidatorInterceptor = modelValidatorInterceptor;
            return this;
        }
    }
}