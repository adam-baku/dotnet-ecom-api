using Common.Price;
using Product.Application.Exception;
using Product.Domain;
using System;

namespace Product.Application.Command
{
    public class CreateProductCommandHandler
    {
        private readonly IProductRepository repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreateProductCommand command)
        {
            if (repository.ProductExists(command.Title)) {
                throw ProductAlreadyExistsException.TitleUnique();
            }

            Domain.Product product = new Domain.Product(
                command.Title,
                command.AvailableQuantity,
                new Price(command.NetPrice, new Tax(command.TaxRate), command.Currency)
            );

            repository.Persist(product);
        }
    }
}
