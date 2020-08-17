using ECommerce.Business.Abstract.Domain.Catalog;
using ECommerce.Business.Abstract.Order;
using ECommerce.Entities.Order;
using ECommerce.WebApi.Filters.Order;
using ECommerce.WebApi.Models;
using ECommerce.WebApi.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IProductService _productService;

        public ShoppingCartItemController(IShoppingCartItemService shoppingCartItemService, IProductService productService)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _productService = productService;
        }

        [HttpGet]
        [ShoppingCartItemException]
        [Authorize(Roles = "admin")]
        //tüm kullanıcılara ait sepetler
        public IActionResult Get()
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>
            {
                Entities = _shoppingCartItemService.GetAll(),
                IsSuccess = true
            };
            response.EntitiesCount = response.Entities.Count();

            return Ok(response);
        }

        [HttpGet("{customerUsername}")]
        [ShoppingCartItemException]
        [Authorize(Roles = "admin, customer")]
        //ilgili kullanıcıya ait sepet
        public IActionResult Get(string customerUsername)
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>();

            response.Entities = _shoppingCartItemService.GetEx(s => s.CustomerUserName == customerUsername);
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpPost]
        [ShoppingCartItemException]
        [ShoppingCartItemValidate]
        [Authorize(Roles = "admin, customer")]
        //sepete ürün ekleme
        public IActionResult Post([FromBody] ShoppingCartItemModel model)
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>();

            var selectedProduct = _productService.GetById(model.ProductId);

            if (selectedProduct == null)
            {
                response.HasError = true;
                response.Errors.Add("Selected Product Does Not Exist!");

                return BadRequest(response);
            }
            else if (selectedProduct.Deleted || !selectedProduct.Published)
            {
                response.HasError = true;
                response.Errors.Add("Selected Product Does Not Publish!");

                return BadRequest(response);
            }
            else if (selectedProduct.StockQuantity <= 0 || selectedProduct.StockQuantity <= selectedProduct.MinStockQuantity)
            {
                response.HasError = true;
                response.Errors.Add("Selected Product Has No Stock!");

                return BadRequest(response);
            }
            else if (selectedProduct.StockQuantity - model.Quantity <= selectedProduct.MinStockQuantity)
            {
                response.HasError = true;
                response.Errors.Add("Stock Of Selected Product Is Not Enough!");

                return BadRequest(response);
            }

            var shoppingCartItem = _shoppingCartItemService.GetEx(s => s.CustomerUserName == model.CustomerUsername && s.ProductId == model.ProductId).FirstOrDefault();

            if (shoppingCartItem == null)
            {
                ShoppingCartItem newShoppingCartItem = new ShoppingCartItem
                {
                    CustomerUserName = model.CustomerUsername,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity
                };

                _shoppingCartItemService.AddToCart(model.CustomerUsername, newShoppingCartItem);

                response.Entity = _shoppingCartItemService.GetById(newShoppingCartItem.Id);
            }
            else
            {
                shoppingCartItem.Quantity += model.Quantity;
                _shoppingCartItemService.AddToCart(model.CustomerUsername, shoppingCartItem);

                response.Entity = _shoppingCartItemService.GetById(shoppingCartItem.Id);
            }

            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpPut]
        [ShoppingCartItemException]
        [ShoppingCartItemValidate]
        [Authorize(Roles = "admin, customer")]
        //sepetten ürün çıkarma
        public IActionResult Put(int id, [FromBody] ShoppingCartItemModel model)
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>();

            var selectedProduct = _productService.GetById(model.ProductId);

            if (selectedProduct == null)
            {
                response.HasError = true;
                response.Errors.Add("Selected Product Does Not Exist!");

                return BadRequest(response);
            }
            else if (selectedProduct.Deleted || !selectedProduct.Published)
            {
                response.HasError = true;
                response.Errors.Add("Selected Product Does Not Publish!");

                return BadRequest(response);
            }

            ShoppingCartItem shoppingCartItem = _shoppingCartItemService.GetById(id);

            if (shoppingCartItem == null)
            {
                response.HasError = true;
                response.Errors.Add("Shopping Cart Item Does Not Exist!");

                return BadRequest(response);
            }

            _shoppingCartItemService.RemoveFromCart(shoppingCartItem);

            response.Entity = _shoppingCartItemService.GetById(id);
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpDelete]
        [ShoppingCartItemException]
        [Authorize(Roles = "admin, customer")]
        //sepeti boşaltma
        public IActionResult Delete(string customerUsername)
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>();

            List<ShoppingCartItem> shoppingCartItem = _shoppingCartItemService.GetEx(s => s.CustomerUserName == customerUsername).ToList();

            if (shoppingCartItem == null || shoppingCartItem.Any())
            {
                response.HasError = true;
                response.Errors.Add("Shopping Cart Item Does Not Exist!");

                return BadRequest(response);
            }

            _shoppingCartItemService.ClearCart(shoppingCartItem);

            response.IsSuccess = true;

            return Ok(response);
        }
    }
}
