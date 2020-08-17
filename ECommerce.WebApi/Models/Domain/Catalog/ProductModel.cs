using System.ComponentModel.DataAnnotations;

namespace ECommerce.WebApi.Models.Domain.Catalog
{
    public class ProductModel
    {
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price Is Required")]
        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        [Required(ErrorMessage = "Sku Is Required")]
        public string Sku { get; set; }

        [Required(ErrorMessage = "Gtin Is Required")]
        public string Gtin { get; set; }

        [Required(ErrorMessage = "Stock Quantity Is Required")]
        public int StockQuantity { get; set; }

        public int MinStockQuantity { get; set; }

        public int DisplayOrder { get; set; }

        public bool Published { get; set; }

        [Required(ErrorMessage = "Category Id Is Required")]
        public int CategoryId { get; set; }
    }
}
