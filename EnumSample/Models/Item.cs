
using System.ComponentModel.DataAnnotations;

namespace OfficeSupply.Models
{
    /// <summary>
    /// Basic information of Item
    /// </summary>
    public class Item
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// Category : Refer to Category
        /// </summary>
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Variety : Number variable of different varities
        /// </summary>
        [Required]
        public string Variety { get; set; }

        /// <summary>
        /// Image FileName saved on server
        /// </summary>
        public string ItemImage { get; set; }

        public virtual string PrevItemImage { get; set; }
    }
}