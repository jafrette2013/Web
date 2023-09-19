using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Program

    {
        [Required]
        [Range(0, 30)]
        public int Id { get; set; }

        [MaxLength(10)]
        [Required]
        public string Name { get; set; }

     
    }
}
