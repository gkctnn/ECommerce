using ECommerce.Core.Entities;
using ECommerce.Entities.Domain.Catalog;
using System;

namespace ECommerce.Entities.Order
{
    public class ShoppingCartItem : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Product Product { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int Id { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
