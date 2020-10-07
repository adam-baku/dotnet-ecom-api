using Common.Price;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Command
{
    public struct CreateProductCommand
    {
        public string Title { get; private set; }
        public int AvailableQuantity { get; private set; }
        public double NetPrice { get; private set; }
        public double TaxRate { get; private set; }
        public Currency Currency { get; private set; }

        public CreateProductCommand(string title, int availableQuantity, double netPrice, double taxRate, Currency currency)
        {
            Title = title;
            AvailableQuantity = availableQuantity;
            NetPrice = netPrice;
            TaxRate = taxRate;
            Currency = currency;
        }
    }
}
