using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Student
    {
        [Required]
        [Range(0, 30)]
        public int Id { get; set; }

        [MaxLength(10)]
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [MaxLength(10)] 
        public string? Email { get; set;}
    }
}
