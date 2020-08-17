using ECommerce.Entities.Domain.Catalog;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.WebApi.Models.Order
{
    public class ShoppingCartItemModel
    {
        [Required(ErrorMessage = "Customer Username Is Required")]
        public string CustomerUsername { get; set; }

        [Required(ErrorMessage = "Product Id Is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity Is Required")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
