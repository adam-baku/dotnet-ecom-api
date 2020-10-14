using Common;
using Common.Price;
using Product.Application.Exception;
using Product.Domain;
using System;
using System.Threading.Tasks;

namespace Product.Application.Command
{
    public class CreateProductCommandHandler : ICommandHandlerAsync<CreateProductCommand>
    {
        private readonly IProductRepository repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(CreateProductCommand command)
        {
            if (await repository.ProductExistsAsync(command.Title)) {
                throw ProductAlreadyExistsException.TitleUnique();
            }

            Domain.Product product = new Domain.Product(
                command.Title,
                command.AvailableQuantity,
                new Price(command.NetPrice, new Tax(command.TaxRate), command.Currency)
            );

            await repository.AddAsync(product);
        }
    }
}
