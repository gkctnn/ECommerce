using ECommerce.Entities.Domain.Catalog;
using ECommerce.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.WebApi.Filters.Domain.Catalog
{
    public class ProductExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ServiceResponse<Product> response = new ServiceResponse<Product>
            {
                HasError = true,
            };
            response.Errors.Add("Exception: " + context.Exception.Message);

            context.Result = new BadRequestObjectResult(response);
        }
    }
}
