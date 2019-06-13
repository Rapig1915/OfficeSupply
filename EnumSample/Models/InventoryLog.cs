using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeSupply.Models
{
    /// <summary>
    /// Basic Information of Inventory
    /// </summary>
    public class InventoryLog
    {
        public int Id { get; set; }
        /// <summary>
        /// Inventory id : Refer to Inventory
        /// </summary>
        public int InventoryId { get; set; }

        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Item id : Refer to Item
        /// </summary>
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        /// <summary>
        /// Flag if Item In/Out from Inventory
        /// </summary>
        public bool InOrOut { get; set; }

        /// <summary>
        /// Current Item count in Inventory
        /// </summary>
        [Required(ErrorMessage = "Count is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Count must be a positive number")]
        public int Count { get; set; }

        /// <summary>
        /// Datetime action performed
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; }
    }
}