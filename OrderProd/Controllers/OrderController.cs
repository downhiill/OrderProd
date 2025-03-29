using Microsoft.AspNetCore.Mvc;
using OrderProd.Model.Request;
using System.ComponentModel.DataAnnotations;

namespace OrderProd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public OrderController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException(); 
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(PostProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(PutProductRequest request)
        {

            if (string.IsNullOrEmpty(request.Name) || request.Price <= 0)
            {
                return BadRequest("Название и цена продукта обязательны и цена должна быть больше нуля.");
            }

            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return NotFound("Продукт не найден.");
            }

            product.Name = request.Name;
            product.Price = request.Price;

            await _productRepository.UpdateAsync(product);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
