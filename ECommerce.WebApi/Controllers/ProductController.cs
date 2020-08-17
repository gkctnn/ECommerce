using ECommerce.Business.Abstract.Domain.Catalog;
using ECommerce.Entities.Domain.Catalog;
using ECommerce.WebApi.Filters.Domain.Catalog;
using ECommerce.WebApi.Models;
using ECommerce.WebApi.Models.Domain.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProductException]
        public IActionResult Get()
        {
            ServiceResponse<Product> response = new ServiceResponse<Product>
            {
                Entities = _productService.GetAll(),
                IsSuccess = true
            };
            response.EntitiesCount = response.Entities.Count();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProductException]
        public IActionResult Get(int id)
        {
            ServiceResponse<Product> response = new ServiceResponse<Product>
            {
                Entity = _productService.GetById(id),
                IsSuccess = true
            };

            return Ok(response);
        }

        [HttpPost]
        [ProductException]
        [ProductValidate]
        [Authorize(Roles = "admin")]
        public IActionResult Post([FromBody] ProductModel model)
        {
            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                DisplayOrder = model.DisplayOrder,
                Gtin = model.Gtin,
                MinStockQuantity = model.MinStockQuantity,
                OldPrice = model.OldPrice,
                Published = model.Published,
                Sku = model.Sku,
                StockQuantity = model.StockQuantity,
                CategoryId = model.CategoryId
            };

            _productService.Insert(product);

            ServiceResponse<Product> response = new ServiceResponse<Product>
            {
                Entity = _productService.GetById(product.Id),
                IsSuccess = true
            };

            return Ok(response);
        }

        [HttpPut]
        [ProductException]
        [ProductValidate]
        [Authorize(Roles = "admin")]
        public IActionResult Put(int id, [FromBody] ProductModel model)
        {
            ServiceResponse<Product> response = new ServiceResponse<Product>();

            Product product = _productService.GetById(id);

            if (product == null)
            {
                response.HasError = true;
                response.Errors.Add("Product Does Not Exist!");

                return BadRequest(response);
            }

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.DisplayOrder = model.DisplayOrder;
            product.Gtin = model.Gtin;
            product.MinStockQuantity = model.MinStockQuantity;
            product.OldPrice = model.OldPrice;
            product.Published = model.Published;
            product.Sku = model.Sku;
            product.StockQuantity = model.StockQuantity;
            product.CategoryId = model.CategoryId;

            _productService.Update(product);

            response.Entity = _productService.GetById(id);
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpDelete]
        [ProductException]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            ServiceResponse<Product> response = new ServiceResponse<Product>();

            Product product = _productService.GetById(id);

            if (product == null)
            {
                response.HasError = true;
                response.Errors.Add("Product Does Not Exist!");

                return BadRequest(response);
            }

            _productService.Delete(product);

            response.Entity = _productService.GetById(id);
            response.IsSuccess = true;

            return Ok(response);
        }
    }
}
