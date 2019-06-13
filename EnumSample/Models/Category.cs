
using System.ComponentModel.DataAnnotations;

namespace OfficeSupply.Models
{
    /// <summary>
    /// Basic Category information for items
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        /// <summary>
        /// Category Name
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}