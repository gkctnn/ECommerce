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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [CategoryException]
        public IActionResult Get()
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>
            {
                Entities = _categoryService.GetAll(),
                IsSuccess = true
            };
            response.EntitiesCount = response.Entities.Count();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [CategoryException]
        public IActionResult Get(int id)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>
            {
                Entity = _categoryService.GetById(id),
                IsSuccess = true
            };

            return Ok(response);
        }

        [HttpPost]
        [CategoryException]
        [CategoryValidate]
        [Authorize(Roles = "admin")]
        public IActionResult Post([FromBody] CategoryModel model)
        {
            Category category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                DisplayOrder = model.DisplayOrder,
                Published = model.Published
            };

            _categoryService.Insert(category);

            ServiceResponse<Category> response = new ServiceResponse<Category>
            {
                Entity = _categoryService.GetById(category.Id),
                IsSuccess = true
            };

            return Ok(response);
        }

        [HttpPut]
        [CategoryException]
        [CategoryValidate]
        [Authorize(Roles = "admin")]
        public IActionResult Put(int id, [FromBody] CategoryModel model)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();

            Category category = _categoryService.GetById(id);

            if (category == null)
            {
                response.HasError = true;
                response.Errors.Add("Category Does Not Exist!");

                return BadRequest(response);
            }

            category.Name = model.Name;
            category.Description = model.Description;
            category.DisplayOrder = model.DisplayOrder;
            category.Published = model.Published;

            _categoryService.Update(category);

            response.Entity = _categoryService.GetById(id);
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpDelete]
        [CategoryException]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();

            Category category = _categoryService.GetById(id);

            if (category == null)
            {
                response.HasError = true;
                response.Errors.Add("Category Does Not Exist!");

                return BadRequest(response);
            }

            _categoryService.Delete(category);

            response.Entity = _categoryService.GetById(id);
            response.IsSuccess = true;

            return Ok(response);
        }
    }
}
