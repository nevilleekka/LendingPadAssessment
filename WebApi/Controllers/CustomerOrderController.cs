using BusinessEntities;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private iProductRepository ProductRepository { get; init; }
        private iOrderRepository OrderRepository { get; init; }

        public CustomerOrderController(iProductRepository productRepository, iOrderRepository orderRepository)
        {
            this.ProductRepository = productRepository;
            this.OrderRepository = orderRepository;
        }

        [HttpGet("AllProducts")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = ProductRepository.GetAll();
            return Ok(products);
        }


        [HttpGet("AllOrders")]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            var orders = OrderRepository.GetAll();
            return Ok(orders);
        }

        [HttpGet("CustomerOrder")]
        public ActionResult<Order> GetCustomerOrder([FromQuery,Required] string customer)
        {

            var order = OrderRepository.Get(customer);
            if (order == null)
                return NotFound($"No orders found for customer '{customer}'.");

            return Ok(order);
        }

            [HttpGet("Order")]
        public ActionResult<Order> GetOrder([FromQuery,Required] string id)
        {
            if (!Guid.TryParse(id, out var orderId))
                return BadRequest("Invalid order id format.");

            var order = OrderRepository.Get(orderId);
            if (order == null)
                return NotFound($"Order with id {id} not found.");

            return Ok(order);
        }


        [HttpGet("Product")]
        public ActionResult<string> GetProduct([FromQuery, Required] string id)
        {
 

            if (!Guid.TryParse(id, out var productId))
                return BadRequest("Invalid product id format.");

            var product = ProductRepository.Get(productId);
            if (product == null)
                return NotFound($"Product with id {id} not found.");

            return Ok(product);
        }


        [HttpPost(nameof(CreateProduct))]
        public ActionResult<Product> CreateProduct([FromForm, Required] string name, [FromForm, Required] string? description)
        {

            var product = new Product
            {
                Name = name,
                Description = description ?? String.Empty
            };

            product = ProductRepository.Create(product);
            return Ok(product);
        }



        [HttpPost(nameof(CreateOrder))]
        public IActionResult CreateOrder([FromForm, Required] string productName, [FromForm, Required] string customerName)
        {

            // Find the product by name
            var product = ProductRepository.Get(productName);
            if (product == null)
                return NotFound($"Product with name '{productName}' not found.");

            // Create the order
            var order = new Order
            {
                Customer = customerName,
                ProductId = product.Identifier,
            };

            var createdOrder = OrderRepository.Create(order);
            return Ok(createdOrder);
        }



        [HttpDelete(nameof(DeleteCustomerOrder))]
        public IActionResult DeleteCustomerOrder([FromQuery, Required] string customer)
        {
            var deletedCount = OrderRepository.Delete(customer);
            if (deletedCount == 0)
                return NotFound($"No orders found for customer '{customer}'.");

            return Ok($"{deletedCount} order(s) deleted for customer '{customer}'.");
        }

        [HttpDelete(nameof(DeleteAllOrders))]
        public IActionResult DeleteAllOrders()
        {
            var deletedCount = OrderRepository.DeleteAll();
            return Ok($"{deletedCount} order(s) deleted.");
        }


        [HttpDelete(nameof(DeleteAllProducts))]
        public IActionResult DeleteAllProducts()
        {
            var deletedCount = ProductRepository.DeleteAll();
            return Ok($"{deletedCount} products(s) deleted.");
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct([FromQuery] string id)
        {
            // Convert int id to Guid if possible, otherwise return BadRequest
            if (!Guid.TryParse(id.ToString(), out var productId))
                return BadRequest("Invalid product id format.");

            var product = ProductRepository.Get(productId);
            if (product == null)
                return NotFound($"Product with id {id} not found.");

            var deleted = ProductRepository.Delete(product);
            if (!deleted)
                return StatusCode(500, "Failed to delete product.");

            return Ok("Product Deleted");
        }
    }
}
