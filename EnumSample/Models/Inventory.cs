using System.ComponentModel.DataAnnotations;

namespace OfficeSupply.Models
{
    /// <summary>
    /// Basic Information of Inventory
    /// </summary>
    public class Inventory
    {
        public int Id { get; set; }

        /// <summary>
        /// Name of inventory
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Description of Inventory
        /// </summary>
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Description { get; set; }
    }
}