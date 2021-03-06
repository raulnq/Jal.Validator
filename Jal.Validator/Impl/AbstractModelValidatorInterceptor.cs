﻿using System;
using Jal.Validator.Interface;
using Jal.Validator.Model;

namespace Jal.Validator.Impl
{
    public abstract class AbstractModelValidatorInterceptor : IModelValidatorInterceptor
    {
        public static IModelValidatorInterceptor Instance = new NullModelValidatorInterceptor(); 

        public void OnEnter<T>(T instance)
        {

        }

        public void OnSuccess<T>(T instance, ValidationResult result)
        {

        }

        public void OnError<T>(T instance, Exception exception)
        {

        }

        public void OnExit<T>(T instance)
        {

        }
    }
}