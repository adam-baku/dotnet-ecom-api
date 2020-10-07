using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Exception
{
    public class ProductAlreadyExistsException : ApplicationException
    {
        public ProductAlreadyExistsException(string? message) : base(message)
        {

        }

        public static ProductAlreadyExistsException TitleUnique()
        {
            return new ProductAlreadyExistsException("Title of product must be uniqe.");
        }
    }
}
