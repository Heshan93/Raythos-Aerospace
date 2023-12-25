//using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace Raythos_Aerospace.Models
{
    public class Product
    {
        
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        public double Price { get; set; }


    }
}
