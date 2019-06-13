
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeSupply.Models
{
    /// <summary>
    /// Stock Result of inventories
    /// </summary>
    public class InventoryStock
    {
        public int Id { get; set; }
        /// <summary>
        /// Inventory id : Refer to Inventory
        /// </summary>
        [Index("Inventory_Item_Stock_Unique", 1, IsUnique = true)]
        public int InventoryId { get; set; }

        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Item id : Refer to Item
        /// </summary>
        [Index("Inventory_Item_Stock_Unique", 2, IsUnique = true)]
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        /// <summary>
        /// Current Item count in Inventory
        /// </summary>
        [Required(ErrorMessage = "Count is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Count must be a positive number")]
        public int Count { get; set; }
    }
}