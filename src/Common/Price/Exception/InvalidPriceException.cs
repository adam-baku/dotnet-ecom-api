using Common.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Price.Exception
{
    public class InvalidPriceException : DomainException
    {
        private InvalidPriceException(string? message) : base(message)
        {

        }

        public static InvalidPriceException TaxMismatch()
        {
            return new InvalidPriceException("Prices must have the same tax");
        }

        public static InvalidPriceException CurrencyMismatch()
        {
            return new InvalidPriceException("Prices must have the same currency");
        }
        public static InvalidPriceException IllegalSubtract()
        {
            return new InvalidPriceException("Higher price can\'t be subtracted from lower price");
        }
    }
}
