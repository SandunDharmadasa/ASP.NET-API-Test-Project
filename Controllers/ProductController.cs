using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test3.Models;

namespace Test3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public Product Post(Product product)
        {
            sampleContext sampleContext = new sampleContext();
            sampleContext.Products.Add(product);
            sampleContext.SaveChanges();
            return product;
        }

        [HttpGet]
        public List<Product> Get()
        {
            sampleContext sampleContext = new sampleContext();
            return sampleContext.Products.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Product updatedProduct) 
        {
            using (var sampleContext = new sampleContext()) 
            {
                var existingProduct = sampleContext.Products.Find(id);
                
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Quantity = updatedProduct.Quantity;
                existingProduct.Status = updatedProduct.Status;

                sampleContext.SaveChanges();
                return Ok(existingProduct);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            using (var sampleContext = new sampleContext()) 
            {
                var productToDelete = sampleContext.Products.Find(id);

                if(productToDelete == null) 
                {
                    return NotFound();
                }

                sampleContext.Products.Remove(productToDelete);
                sampleContext.SaveChanges();
                return NoContent();
            }
        }
    }
}
