using Common.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Exception
{
    public class InvalidQuantityException : DomainException
    {
        private InvalidQuantityException(string message) : base(message)
        {

        }

        public static InvalidQuantityException NonPositiveValue()
        {
            return new InvalidQuantityException("Quantity of product must be positive.");
        }
    }
}
