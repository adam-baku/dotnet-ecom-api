using Common.Price.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Price
{
    public class Price
    {
        public double Net { get; private set; }
        public double Gross { get; private set; }
        public Tax Tax { get; private set; }
        public Currency Currency { get; private set; }

        public Price(double net, Tax tax, Currency currency)
        {
            Net = Math.Round(net, 2);
            Gross = Math.Round(Net + (Net * (tax.Value / 100)), 2);
            Tax = tax;
            Currency = currency;
        }

        public static Price operator *(Price price, int multiplier)
        {
            return new Price(
                price.Net * multiplier,
                price.Tax,
                price.Currency
            );
        }

        public static Price operator +(Price left, Price right)
        {
            if (left.Tax != right.Tax) {
                throw InvalidPriceException.TaxMismatch();
            }

            if (left.Currency != right.Currency) {
                throw InvalidPriceException.CurrencyMismatch();
            }

            return new Price(
                left.Net + right.Net,
                left.Tax,
                left.Currency
            );
        }

        public static Price operator -(Price left, Price right)
        {
            if (right.Net > left.Net) {
                throw InvalidPriceException.IllegalSubtract();
            }

            if (left.Tax != right.Tax) {
                throw InvalidPriceException.TaxMismatch();
            }

            if (left.Currency != right.Currency) {
                throw InvalidPriceException.CurrencyMismatch();
            }

            return new Price(
                left.Net - right.Net,
                left.Tax,
                left.Currency
            );
        }
    }
}
