using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Price;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Command;
using Web.DTO;
using Web.Filters;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        [Route("new")]
        [Consumes("application/json")]
        public async Task AddNewProduct(NewProductDTO newProduct, [FromServices] CreateProductCommandHandler handler)
        {
            var command = new CreateProductCommand(
                newProduct.Title,
                newProduct.Quantity,
                newProduct.NetPrice,
                newProduct.Tax,
                Enum.Parse<Currency>(newProduct.Currency)
            );

            //should be dispatched by message bus
            await handler.HandleAsync(command);
        }
    }
}
