
using System.ComponentModel.DataAnnotations;

namespace OfficeSupply.Models
{
    /// <summary>
    /// Stock Result of inventories
    /// </summary>
    public class ItemImage
    {
        public int Id { get; set; }

        /// <summary>
        /// Item id : Refer to Item
        /// </summary>
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        /// <summary>
        /// ImageURL for current Item
        /// </summary>
        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; }
    }
}