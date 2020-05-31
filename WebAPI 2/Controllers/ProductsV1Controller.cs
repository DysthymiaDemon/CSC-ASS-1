using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WebAPI_2.Models;

namespace WebAPI_2.Controllers
{
    public class ProductsV1Controller : ApiController
    {
        // static readonly IProductRepository repository = new ProductRepository();

        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        //code for version 1


        [HttpGet]
        [Route("api/v1/products/version")]
        //http://localhost:9000/api/v1/products/version
        public string[] GetVersion()
        {
            return new string[]
              {"hello",
                  "version 2",
                  "2"
              };
        }


        [HttpGet]
        [Route("api/v1/products/message/")]
        //http://localhost:9000/api/v1/products/message?name1=ji&name2=jii1&name3=ji3
        public HttpResponseMessage GetMultipleNames(String name1, string name2, string name3)
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent("<html><body>Hello World " +
                    " name1 =" + name1 +
                    " name2= " + name2 +
                    " name3=" + name3 +
                    " is provided in path parameter</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }







        [HttpGet]
        [Route("api/v1/products")]
        //http://localhost:9000/api/v1/products
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }


        [HttpGet]
        [Route("api/v1/products/{id:int:min(2)}")]
        //http://localhost:9000/api/v1/products/3
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
