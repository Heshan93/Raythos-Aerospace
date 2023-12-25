using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Raythos_Aerospace.Models
{
    public class Catagory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("DisplayOrder")]
        [Range(1,100,ErrorMessage =" out of the range(1 to  100)")]
        public string DisplayOrder { get; set; } 
    }
}
