using ECommerce.Entities.Domain.Catalog;
using ECommerce.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.WebApi.Filters.Domain.Catalog
{
    public class CategoryExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>
            {
                HasError = true,
            };
            response.Errors.Add("Exception: " + context.Exception.Message);

            context.Result = new BadRequestObjectResult(response);
        }
    }
}
