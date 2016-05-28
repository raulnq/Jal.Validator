using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jal.Validator.Model;

namespace Jal.Validator.Interface
{
    public interface IModelValidatorInterceptor
    {
        void OnEnter<T>(T instance);

        void OnSuccess<T>(T instance, ValidationResult result);

        void OnError<T>(T instance, Exception exception);

        void OnExit<T>(T instance);
    }
}
