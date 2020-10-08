using subject = Product.Domain;
using Product.Domain.Exception;
using System;
using Xunit;
using Common.Price;

namespace ProductTest.Domain
{
    public class ProductTest
    {
        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void WillThrowExceptionWhenCreateProductWithNotPositiveQuantity(int givenNonPositiveQuantity)
        {
            //given
            string title = "Some Title";
            Price price = new Price(10.00, new Tax(10.00), Currency.PLN);

            //when
            //then
            var exception = Assert.Throws<InvalidQuantityException>(
                () => new subject::Product(title, givenNonPositiveQuantity, price)
            );

            Assert.Equal("Quantity of product must be positive.", exception.Message);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void WillThrowExceptionWhenChangeQuantityWithNotPositiveQuantity(int givenNonPositiveQuantity)
        {
            //given
            string title = "Some Title";
            int positiveQuantity = 10;
            Price price = new Price(10.00, new Tax(10.00), Currency.PLN);

            subject::Product product = new subject::Product(title, positiveQuantity, price);

            //when
            //then
            var exception = Assert.Throws<InvalidQuantityException>(
                () => product.ChangeQuantity(givenNonPositiveQuantity) //when
            );

            Assert.Equal("Quantity of product must be positive.", exception.Message);
        }

        [Theory]
        [InlineData(10, 0)] //equals
        [InlineData(5, 5)] //less
        public void WillSubtractQuantityWhenItLessOrEqualsAvailableQuantityAndPositive(int givenQuantityToSubtract, int expectedQuantityLeft)
        {
            //given
            string title = "Some Title";
            int availableQuantity = 10;
            Price price = new Price(10.00, new Tax(10.00), Currency.PLN);

            subject::Product product = new subject::Product(title, availableQuantity, price);

            //when
            product.SubtractQuantity(givenQuantityToSubtract);

            //then
            Assert.Equal(expectedQuantityLeft, product.AvailableQuantity);
        }

        [Fact]
        public void WillThrowExceptionWhenQuantityToSubtractIsHigherThanAvailable()
        {
            //given
            string title = "Some Title";
            int availableQuantity = 10;
            int quantityToSubtract = 11;
            Price price = new Price(10.00, new Tax(10.00), Currency.PLN);

            subject::Product product = new subject::Product(title, availableQuantity, price);

            //when
            //then
            var exception = Assert.Throws<SubtractionNotAllowedException>(
                () => product.SubtractQuantity(quantityToSubtract) //when
            );

            Assert.Equal(@"Can't subtract more than available.", exception.Message);
        }

        [Theory]
        [InlineData(0)] //non positive
        [InlineData(-10)] //non positive
        public void WillThrowExceptionWhenQuantityToSubtractIsNonPositive(int givenQuantityToSubtract)
        {
            //given
            string title = "Some Title";
            int availableQuantity = 10;
            Price price = new Price(10.00, new Tax(10.00), Currency.PLN);

            subject::Product product = new subject::Product(title, availableQuantity, price);

            //when
            //then
            var exception = Assert.Throws<InvalidQuantityException>(
                () => product.SubtractQuantity(givenQuantityToSubtract) //when
            );

            Assert.Equal("Quantity of product must be positive.", exception.Message);
        }
    }
}
