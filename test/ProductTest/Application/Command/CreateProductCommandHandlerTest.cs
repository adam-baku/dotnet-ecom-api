using Product.Application.Command;
using System;
using Xunit;
using Moq;
using Product.Domain;
using Product.Application.Exception;

namespace ProductTest.Application.Command
{
    public class CreateProductCommandHandlerTest
    {
        [Fact]
        public void WillCreateAndPersistNewProduct()
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
            productRepositoryMock.Setup(repository => repository.ProductExists(It.IsAny<string>())).Returns(false);
            productRepositoryMock.Setup(repository => repository.Persist(It.IsAny<Product.Domain.Product>()));

            //when
            CreateProductCommandHandler handler = new CreateProductCommandHandler(productRepositoryMock.Object);
            handler.Handle(command);

            //then
            productRepositoryMock.Verify(repository => repository.ProductExists(It.IsAny<string>()), Times.Once());
            productRepositoryMock.Verify(repository => repository.Persist(It.IsAny<Product.Domain.Product>()), Times.Once());
        }

        [Fact]
        public void WillThrowExceptionWhenProductWithSameNameExists()
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
            productRepositoryMock.Setup(repository => repository.ProductExists(It.IsAny<string>())).Returns(true);

            //when
            CreateProductCommandHandler handler = new CreateProductCommandHandler(productRepositoryMock.Object);
            
            //then
            var exception = Assert.Throws<ProductAlreadyExistsException>(
                () => handler.Handle(command) //when
            );

            Assert.Equal("Title of product must be uniqe.", exception.Message);
            productRepositoryMock.Verify(repository => repository.Persist(It.IsAny<Product.Domain.Product>()), Times.Never());
        }
    }
}
