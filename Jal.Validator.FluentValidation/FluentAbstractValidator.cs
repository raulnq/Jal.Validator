﻿using System.Collections.Generic;
using FluentValidation;
using Jal.Validator.Model;

namespace Jal.Validator.FluentValidation
{
    public abstract class FluentAbstractValidator<TTarget> : global::FluentValidation.AbstractValidator<TTarget>, Interface.IValidator<TTarget>
    {
        ValidationResult Interface.IValidator<TTarget>.Validate(TTarget instance)
        {
            return ValidateInstance(instance, string.Empty);
        }

        ValidationResult Interface.IValidator<TTarget>.Validate(TTarget instance, string subgroup)
        {
            return ValidateInstance(instance, subgroup);
        }
   
        ValidationResult Interface.IValidator<TTarget>.Validate(TTarget instance, dynamic context)
        {
            return ValidateInstance(instance, string.Empty);
        }

        ValidationResult Interface.IValidator<TTarget>.Validate(TTarget instance, string subgroup, dynamic context)
        {
            return ValidateInstance(instance, subgroup);
        }

        ValidationResult ValidateInstance(TTarget instance, string ruleSet)
        {
            var concreteValidator = this;
            var results = string.IsNullOrEmpty(ruleSet) ? concreteValidator.Validate(instance) : concreteValidator.Validate(instance, ruleSet: ruleSet);
            var messages = new List<ValidationFailure>();
            foreach (var error in results.Errors)
            {
                var splitMessage = error.ErrorMessage.Split(ValidationSettings.TokenMessageSeparator);
                if (splitMessage.Length > 1)
                {
                    messages.Add(new ValidationFailure(error.PropertyName, splitMessage[1], splitMessage[0]));
                }
                else
                {
                    messages.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage));
                }
            }
            return new ValidationResult(messages);
        }

    }
}
