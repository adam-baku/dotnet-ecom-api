using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exception
{
    public class DomainException : RuntimeExceptionAbstract
    {
        protected DomainException() : base()
        {

        }

        protected DomainException(string? message) : base(message)
        {

        }

        public DomainException(string? message, System.Exception? innerException) : base(message, innerException)
        {

        }
    }
}
