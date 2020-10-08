using Common.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Exception
{
    public class SubtractionNotAllowedException : DomainException
    {
        private SubtractionNotAllowedException(string message) : base(message)
        {

        }

        public static SubtractionNotAllowedException MoreThanAvailable()
        {
            return new SubtractionNotAllowedException(@"Can't subtract more than available.");
        }
    }
}
