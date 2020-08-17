using ECommerce.Business.Abstract.Order;
using ECommerce.Entities.Order;
using ECommerce.WebApi.Filters.Order;
using ECommerce.WebApi.Models;
using ECommerce.WebApi.Models.Order;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;

        public ShoppingCartItemController(IShoppingCartItemService shoppingCartItemService)
        {
            _shoppingCartItemService = shoppingCartItemService;
        }

        [HttpGet]
        [ShoppingCartItemException]
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

        [HttpGet("{id}")]
        [ShoppingCartItemException]
        public IActionResult Get(int id)
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>
            {
                Entity = _shoppingCartItemService.GetById(id),
                IsSuccess = true
            };

            return Ok(response);
        }

        [HttpPost]
        [ShoppingCartItemException]
        [ShoppingCartItemValidate]
        public IActionResult Post([FromBody] ShoppingCartItemModel model)
        {
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem
            {
                CustomerId = model.CustomerId,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };

            _shoppingCartItemService.Insert(shoppingCartItem);

            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>
            {
                Entity = _shoppingCartItemService.GetById(shoppingCartItem.Id),
                IsSuccess = true
            };

            return Ok(response);
        }

        [HttpPut]
        [ShoppingCartItemException]
        [ShoppingCartItemValidate]
        public IActionResult Put(int id, [FromBody] ShoppingCartItemModel model)
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>();

            ShoppingCartItem shoppingCartItem = _shoppingCartItemService.GetById(id);

            if (shoppingCartItem == null)
            {
                response.HasError = true;
                response.Errors.Add("Shopping Cart Item Does Not Exist!");

                return BadRequest(response);
            }

            shoppingCartItem.CustomerId = model.CustomerId;
            shoppingCartItem.ProductId = model.ProductId;
            shoppingCartItem.Quantity = model.Quantity;

            _shoppingCartItemService.Update(shoppingCartItem);

            response.Entity = _shoppingCartItemService.GetById(id);
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpDelete]
        [ShoppingCartItemException]
        public IActionResult Delete(int id)
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>();

            ShoppingCartItem shoppingCartItem = _shoppingCartItemService.GetById(id);

            if (shoppingCartItem == null)
            {
                response.HasError = true;
                response.Errors.Add("Categor Does Not Exist!");

                return BadRequest(response);
            }

            _shoppingCartItemService.Delete(shoppingCartItem);

            response.Entity = _shoppingCartItemService.GetById(id);
            response.IsSuccess = true;

            return Ok(response);
        }
    }
}
