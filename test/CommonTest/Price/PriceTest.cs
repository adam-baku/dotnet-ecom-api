using subject = Common.Price;
using System;
using System.Collections.Generic;
using Xunit;
using Common.Price.Exception;

namespace CommonTest.Price
{
    public class PriceTest
    {
        [Theory]
        [MemberData(nameof(NetGrossDataProvider))]
        public void WillRoundNetToTwoDigitsAndCalculateGross(double givenNet, double givenTax, double expectedNet, double expectedGross)
        {
            //when
            subject::Price price = new subject::Price(
                givenNet,
                new subject::Tax(givenTax),
                subject::Currency.PLN
            );

            //then
            Assert.Equal(expectedNet, price.Net);
            Assert.Equal(expectedGross, price.Gross);
        }

        public static IEnumerable<object[]> NetGrossDataProvider =>
            new List<object[]> {
                new object[] { 100.00, 20.00, 100.00, 120.00 },
                new object[] { 150.1111, 8.20, 150.11, 162.42 },
                new object[] { 1.999, 33.33, 2.00, 2.67 },
                new object[] { 59.8719, 0, 59.87, 59.87 }
            };



        [Theory]
        [MemberData(nameof(MultiplyDataProvider))]
        public void WillMultiplyPrice(double givenNet, double givenTax, int multiplier, double expectedMultipliedNet, double expectedMultipliedGross)
        {
            //given
            subject::Price price = new subject::Price(
                givenNet,
                new subject::Tax(givenTax),
                subject::Currency.PLN
            );

            //when
            subject::Price multipliedPrice = price * multiplier;

            //then
            Assert.Equal(expectedMultipliedNet, multipliedPrice.Net);
            Assert.Equal(expectedMultipliedGross, multipliedPrice.Gross);
        }

        public static IEnumerable<object[]> MultiplyDataProvider =>
            new List<object[]> {
                new object[] { 100.00, 20.00, 2, 200.00, 240.00 },
                new object[] { 150.1111, 8.20, 3, 450.33, 487.26 },
                new object[] { 1.999, 33.33, 10, 20.00, 26.67 },
                new object[] { 59.8719, 0, 5, 299.35, 299.35 }
            };




        [Theory]
        [MemberData(nameof(AddDataProvider))]
        public void WillAddTwoPricesWithSameTaxAndCurrency(double givenBasePriceNet, double givenSecondPriceNet, double givenTax, double expectedNet, double expectedGross)
        {
            //given
            subject::Price basePrice = new subject::Price(
                givenBasePriceNet,
                new subject::Tax(givenTax),
                subject::Currency.PLN
            );

            subject::Price secondPrice = new subject::Price(
                givenSecondPriceNet,
                new subject::Tax(givenTax),
                subject::Currency.PLN
            );

            //when
            subject::Price resultPrice = basePrice + secondPrice;

            //then
            Assert.Equal(expectedNet, resultPrice.Net);
            Assert.Equal(expectedGross, resultPrice.Gross);
        }

        public static IEnumerable<object[]> AddDataProvider =>
            new List<object[]> {
                new object[] { 100.00, 50.50, 20.00, 150.50, 180.60 },
                new object[] { 150.1111, 12.9881, 8.20, 163.10, 176.47 },
                new object[] { 1.999, 9.981, 33.33, 11.98, 15.97 },
                new object[] { 59.8719, 34.333, 0, 94.20, 94.20 }
            };



        [Fact]
        public void WillThrowExceptionWhenAddPricesWithDifferentTax()
        {
            //given
            subject::Price basePrice = new subject::Price(
                100.00,
                new subject::Tax(10.00),
                subject::Currency.PLN
            );

            subject::Price secondPrice = new subject::Price(
                100.00,
                new subject::Tax(20.00), //different tax
                subject::Currency.PLN
            );

            //then
            var exception = Assert.Throws<InvalidPriceException>(
                () => basePrice + secondPrice //when
            );

            Assert.Equal("Prices must have the same tax", exception.Message);
        }


        [Fact]
        public void WillThrowExceptionWhenAddPricesWithDifferentCurrency()
        {
            //given
            subject::Price basePrice = new subject::Price(
                100.00,
                new subject::Tax(10.00),
                subject::Currency.PLN
            );

            subject::Price secondPrice = new subject::Price(
                100.00,
                new subject::Tax(10.00),
                subject::Currency.EUR //different currency
            );

            //then
            var exception = Assert.Throws<InvalidPriceException>(
                () => basePrice + secondPrice //when
            );

            Assert.Equal("Prices must have the same currency", exception.Message);
        }




        [Theory]
        [MemberData(nameof(SubtractDataProvider))]
        public void WillSubtractLowerPriceFromHigherWithSameTaxAndCurrency(double givenBasePriceNet, double givenSecondPriceNet, double givenTax, double expectedNet, double expectedGross)
        {
            //given
            subject::Price basePrice = new subject::Price(
                givenBasePriceNet,
                new subject::Tax(givenTax),
                subject::Currency.PLN
            );

            subject::Price secondPrice = new subject::Price(
                givenSecondPriceNet,
                new subject::Tax(givenTax),
                subject::Currency.PLN
            );

            //when
            subject::Price resultPrice = basePrice - secondPrice;

            //then
            Assert.Equal(expectedNet, resultPrice.Net);
            Assert.Equal(expectedGross, resultPrice.Gross);
        }

        public static IEnumerable<object[]> SubtractDataProvider =>
            new List<object[]> {
                new object[] { 100.00, 50.50, 20.00, 49.50, 59.40 },
                new object[] { 150.1111, 12.9881, 8.20, 137.12, 148.36 },
                new object[] { 9.981, 1.999, 33.33, 7.98, 10.64 },
                new object[] { 59.8719, 34.333, 0, 25.54, 25.54 }
            };


        [Fact]
        public void WillThrowExceptionWhenSubtractHigherPriceFromLower()
        {
            //given
            subject::Price higherPrice = new subject::Price(
                100.00,
                new subject::Tax(10.00),
                subject::Currency.PLN
            );

            subject::Price lowerPrice = new subject::Price(
                50.00,
                new subject::Tax(10.00),
                subject::Currency.PLN
            );

            //then
            var exception = Assert.Throws<InvalidPriceException>(
                () => lowerPrice - higherPrice //when
            );

            Assert.Equal("Higher price can\'t be subtracted from lower price", exception.Message);
        }


        [Fact]
        public void WillThrowExceptionWhenSubtractPricesWithDifferentTax()
        {
            //given
            subject::Price basePrice = new subject::Price(
                100.00,
                new subject::Tax(10.00),
                subject::Currency.PLN
            );

            subject::Price secondPrice = new subject::Price(
                100.00,
                new subject::Tax(20.00), //different tax
                subject::Currency.PLN
            );

            //then
            var exception = Assert.Throws<InvalidPriceException>(
                () => basePrice - secondPrice //when
            );

            Assert.Equal("Prices must have the same tax", exception.Message);
        }


        [Fact]
        public void WillThrowExceptionWhenSubtractPricesWithDifferentCurrency()
        {
            //given
            subject::Price basePrice = new subject::Price(
                100.00,
                new subject::Tax(10.00),
                subject::Currency.PLN
            );

            subject::Price secondPrice = new subject::Price(
                100.00,
                new subject::Tax(10.00),
                subject::Currency.EUR //different currency
            );

            //then
            var exception = Assert.Throws<InvalidPriceException>(
                () => basePrice - secondPrice //when
            );

            Assert.Equal("Prices must have the same currency", exception.Message);
        }
    }
}
