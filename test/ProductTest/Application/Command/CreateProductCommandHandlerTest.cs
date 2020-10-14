using Product.Application.Command;
using System;
using Xunit;
using Moq;
using Product.Domain;
using Product.Application.Exception;
using System.Threading.Tasks;

namespace ProductTest.Application.Command
{
    public class CreateProductCommandHandlerTest
    {
        [Fact]
        public async Task WillCreateAndPersistNewProduct()
        {
            //given
            CreateProductCommand command = new CreateProductCommand(
                "Some fancy product title",
                100,
                19.99,
                23.00,
                Common.Price.Currency.PLN
            );

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.ProductExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            productRepositoryMock.Setup(repository => repository.AddAsync(It.IsAny<Product.Domain.Product>()));

            //when
            CreateProductCommandHandler handler = new CreateProductCommandHandler(productRepositoryMock.Object);
            await handler.HandleAsync(command);

            //then
            productRepositoryMock.Verify(repository => repository.ProductExistsAsync(It.IsAny<string>()), Times.Once());
            productRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<Product.Domain.Product>()), Times.Once());
        }

        [Fact]
        public async Task WillThrowExceptionWhenProductWithSameNameExists()
        {
            //given
            CreateProductCommand command = new CreateProductCommand(
                "Some fancy product title",
                100,
                19.99,
                23.00,
                Common.Price.Currency.PLN
            );

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.ProductExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            //when
            CreateProductCommandHandler handler = new CreateProductCommandHandler(productRepositoryMock.Object);
            
            //then
            var exception = await Assert.ThrowsAsync<ProductAlreadyExistsException>(
                () => handler.HandleAsync(command) //when
            );

            Assert.Equal("Title of product must be uniqe.", exception.Message);
            productRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<Product.Domain.Product>()), Times.Never());
        }
    }
}
