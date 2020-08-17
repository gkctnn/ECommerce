using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [UIHint("hidden")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
