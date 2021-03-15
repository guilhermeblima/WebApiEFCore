using System.ComponentModel.DataAnnotations;

namespace ApiEFCore.Models
{
    public class Product
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(60, ErrorMessage = "Title length must be between 3 and 60 characters")]
        [MinLength(3, ErrorMessage = "Title length must be between 3 and 60 characters")]
        public string Title {get; set;}

        [MaxLength(1024, ErrorMessage = "Description must contain less than 1024 characters")]
        public string Description {get; set;}

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price {get; set;}

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Category")]
        public int CategoryId {get; set;}
        public Category Category {get; set;}
        


    }
}