using ECommerce.Entities.Order;
using ECommerce.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.WebApi.Filters.Order
{
    public class ShoppingCartItemExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>
            {
                HasError = true,
            };
            response.Errors.Add("Exception: " + context.Exception.Message);

            context.Result = new BadRequestObjectResult(response);
        }
    }
}
