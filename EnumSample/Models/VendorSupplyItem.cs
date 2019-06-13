
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeSupply.Models
{
    /// <summary>
    /// Matching table of Item-Vendor ( M:N )
    /// </summary>
    public class VendorSupplyItem
    {
        public int Id { get; set; }

        /// <summary>
        /// Refer to Vendor table
        /// </summary>
        [Required]
        [Index("Vendor_Item_Unique", 1, IsUnique = true)]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        /// <summary>
        /// Refer to Item table
        /// </summary>
        [Index("Vendor_Item_Unique", 2, IsUnique = true)]
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}