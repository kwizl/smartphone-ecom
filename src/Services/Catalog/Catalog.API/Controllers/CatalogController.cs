using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(repository));
        }

        // Gets all Products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();    
            
            return Ok(products);
        }

        // Gets Product by ID
        [HttpGet("{ID:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Product))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByID(string ID)
        {
            var product = await _repository.GetProduct(ID);

            if (product == null)
            {
                _logger.LogError($"Product with ID: {ID}, not found");
            }

            return Ok(product);
        }

        // Gets Product by Category
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var product = await _repository.GetProductByCategory(category);

            return Ok(product);
        }

        // Create Product
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.ID }, product);
        }

        // Updates Product
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Product))]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var updated = await _repository.UpdateProduct(product);

            if (updated == false)
            {
                return NotFound(product);
            }
            
            return NoContent();
        }

        // Deletes Product
        [HttpDelete("{ID:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(EmptyResult))]
        public async Task<ActionResult<IEnumerable<Product>>> DeleteProductByID(string ID)
        {
            var product = await _repository.DeleteProduct(ID);

            if (product == false)
            {
                NotFound(product);
            }

            return Ok(product);
        }
    }
}
