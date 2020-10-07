using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain
{
    public interface IProductRepository
    {
        public void Persist(Product product);
        public bool ProductExists(string title);
    }
}
