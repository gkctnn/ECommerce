using ECommerce.Entities.Order;
using ECommerce.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ECommerce.WebApi.Filters.Order
{
    public class ShoppingCartItemValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ServiceResponse<ShoppingCartItem> response = new ServiceResponse<ShoppingCartItem>
                {
                    Errors = context.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList(),
                    HasError = true
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
