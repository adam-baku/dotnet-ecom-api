using Common.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Price.Exception
{
    public class InvalidTaxException : DomainException
    {
        private InvalidTaxException(string? message) : base(message)
        {

        }

        public static InvalidTaxException OutOfBound(double value)
        {
            return new InvalidTaxException($"Value: {value} does not fit between {Tax.Bound.MIN} and {Tax.Bound.MAX}");
        }
    }
}
