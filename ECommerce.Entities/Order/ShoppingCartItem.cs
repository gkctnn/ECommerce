using ECommerce.Core.Entities;
using ECommerce.Entities.Domain.Catalog;
using System;

namespace ECommerce.Entities.Order
{
    public class ShoppingCartItem : IEntity
    {
        #region Properties
        public string CustomerUserName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int Id { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
