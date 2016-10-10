using System;
using Jal.Factory.Interface;
using Jal.Validator.Fluent.Interface;
using Jal.Validator.Impl;
using Jal.Validator.Interface;

namespace Jal.Validator.Fluent.Impl
{
    public class ModelValidatorFluentBuilder : IModelValidatorFluentBuilder, IModelValidatorStartFluentBuilder
    {
        public IValidatorFactory ValidatorFactory;

        public IModelValidatorInterceptor ModelValidatorInterceptor;

        public IModelValidatorFluentBuilder UseFactory(IObjectFactory objectFactory)
        {
            if (objectFactory == null)
            {
                throw new ArgumentNullException(nameof(objectFactory));
            }
            ValidatorFactory = new ValidatorFactory(objectFactory);

            return this;
        }
        public IModelValidator Create
        {
            get
            {

                var result = new ModelValidator(ValidatorFactory);

                if (ModelValidatorInterceptor != null)
                {
                    result.Interceptor = ModelValidatorInterceptor;
                }
                return result;
            }
        }

        public IModelValidatorEndFluentBuilder UseInterceptor(IModelValidatorInterceptor modelValidatorInterceptor)
        {
            if (modelValidatorInterceptor == null)
            {
                throw new ArgumentNullException(nameof(modelValidatorInterceptor));
            }
            ModelValidatorInterceptor = modelValidatorInterceptor;

            return this;
        }
    }
}