//using MessagePack;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public double? Price { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please select an sfdimage")]
        [DisplayName("Product Image")]
        public IFormFile ImageFile { get; set; }

        //[BindNever]
        public string ImagePath { get; set; }

    }
}
