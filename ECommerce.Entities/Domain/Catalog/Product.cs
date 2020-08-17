using ECommerce.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Entities.Domain.Catalog
{
    public class Product : IEntity
    {
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OldPrice { get; set; }
        public string Sku { get; set; }
        public string Gtin { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockQuantity { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public Category Category { get; set; }
        public int Id { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
