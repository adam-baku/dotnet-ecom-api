using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exception
{
    public class ApplicationException : RuntimeExceptionAbstract
    {
        protected ApplicationException() : base()
        {

        }

        protected ApplicationException(string? message) : base(message)
        {

        }

        public ApplicationException(string? message, System.Exception? innerException) : base(message, innerException)
        {

        }
    }
}
