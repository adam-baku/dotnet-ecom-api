using Common.Price.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Price
{
    public class Tax
    {
        public double Value { get; private set; }

        //EF Core
        private Tax() { }

        public Tax(double value)
        {
            if (value < Bound.MIN || value > Bound.MAX) {
                throw InvalidTaxException.OutOfBound(value);
            }

            this.Value = Math.Round(value, 2);
        }

        public static bool operator ==(Tax left, Tax right)
        {
            return left.Value == right.Value;
        }


        public static bool operator !=(Tax left, Tax right)
        {
            return left.Value != right.Value;
        }

        public override string ToString() => this.Value.ToString();

        public interface Bound
        {
            public const double MIN = 0.0;
            public const double MAX = 100.0;
        }
    }
}
