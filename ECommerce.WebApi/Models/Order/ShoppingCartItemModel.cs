using System.ComponentModel.DataAnnotations;

namespace ECommerce.WebApi.Models.Order
{
    public class ShoppingCartItemModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public int Quantity { get; set; }
    }
}
