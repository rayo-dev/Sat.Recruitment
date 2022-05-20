﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Sat.Recruitment.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new List<string>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.AddRange(propertyFailures);
            }
        }

        public List<string> Failures { get; }
    }
}
