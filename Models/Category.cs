using System.ComponentModel.DataAnnotations;

namespace ApiEFCore.Models
{
    public class Category
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(60, ErrorMessage = "Title length must be between 3 and 60 characters")]
        [MinLength(3, ErrorMessage = "Title length must be between 3 and 60 characters")]
        public string Title {get; set;}
    }
}