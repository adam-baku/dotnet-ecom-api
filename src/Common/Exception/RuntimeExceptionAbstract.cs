using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exception
{
    public abstract class RuntimeExceptionAbstract : SystemException
    {
        public RuntimeExceptionAbstract() : base()
        {

        }

        public RuntimeExceptionAbstract(string? message) : base(message)
        {

        }

        public RuntimeExceptionAbstract(string? message, System.Exception? innerException) : base(message, innerException)
        {
            
        }
    }
}
