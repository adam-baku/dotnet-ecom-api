using Common.Price;
using Common.Price.Exception;
using System;
using Xunit;

namespace CommonTest.Price
{
    public class TaxTest
    {
        [Theory]
        [InlineData(0, 0.00)]
        [InlineData(2.2222, 2.22)]
        [InlineData(050.011, 50.01)]
        [InlineData(33.997, 34.00)]
        [InlineData(100.0000, 100.00)]
        public void WillRoundValueToTwoDigits(double givenValue, double expectdValue)
        {
            //when
            Tax tax = new Tax(givenValue);

            //then
            Assert.Equal(expectdValue, tax.Value);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0.001)]
        [InlineData(100.1)]
        [InlineData(101)]
        public void WillThrowExceptionWhenValueIsLessThanZeroOrMoreThanHundred(double givenValue)
        {
            //when
            //then
            Assert.Throws<InvalidTaxException>(() => new Tax(givenValue));
        }
    }
}
