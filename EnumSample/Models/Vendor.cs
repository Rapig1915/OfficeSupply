
using System.ComponentModel.DataAnnotations;

namespace OfficeSupply.Models
{
    /// <summary>
    /// Basic information of vendor
    /// </summary>
    public class Vendor
    {
        public int Id { get; set; }

        /// <summary>
        /// Name of Vendor
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}