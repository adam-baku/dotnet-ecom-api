using Common.Price;
using System;

namespace Product.Domain
{
    public class Product
    {
        public int Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string Title { get; private set; }
        public int AvailableQuantity { get; private set; }
        public Price Price { get; private set; }

        public Product(string title, int availableQuantity, Price price)
        {
            ProductId = Guid.NewGuid();
            Title = title;
            AvailableQuantity = availableQuantity;
            Price = price;
        }
    }
}
