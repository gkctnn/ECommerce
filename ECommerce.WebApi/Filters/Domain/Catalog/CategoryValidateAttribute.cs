﻿using ECommerce.Entities.Domain.Catalog;
using ECommerce.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ECommerce.WebApi.Filters.Domain.Catalog
{
    public class CategoryValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ServiceResponse<Category> response = new ServiceResponse<Category>
                {
                    Errors = context.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList(),
                    HasError = true
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
