using System.ComponentModel.DataAnnotations;

namespace ECommerce.WebApi.Models.Domain.Catalog
{
    public class CategoryModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Description { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }
    }
}
