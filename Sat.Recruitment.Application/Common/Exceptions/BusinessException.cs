using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Sat.Recruitment.Application.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base($"Business exception \"{message}\".")
        {
        }
    }
}
