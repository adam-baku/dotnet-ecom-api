using Common.Price;
using Product.Domain.Exception;
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
            AssertQuantity(availableQuantity);

            ProductId = Guid.NewGuid();
            Title = title;
            AvailableQuantity = availableQuantity;
            Price = price;
        }

        public void ChangeTitle(string newTitle)
        {
            Title = newTitle;
        }

        public void ChangePrice(Price newPrice)
        {
            Price = newPrice;
        }

        public void ChangeQuantity(int newQuantity)
        {
            AssertQuantity(newQuantity);

            AvailableQuantity = newQuantity;
        }

        public void SubtractQuantity(int quantity)
        {
            AssertQuantity(quantity);

            if (!CanSubtract(quantity)) {
                throw SubtractionNotAllowedException.MoreThanAvailable();
            }

            AvailableQuantity -= quantity;
        }

        public bool CanSubtract(int quantity)
        {
            return quantity <= AvailableQuantity;
        }

        private void AssertQuantity(int quantity)
        {
            if (quantity <= 0) {
                throw InvalidQuantityException.NonPositiveValue();
            }
        }
    }
}
