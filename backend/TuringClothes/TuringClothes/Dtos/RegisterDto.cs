using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TuringClothes.Dtos
{
    [Index(nameof(Email), IsUnique = true)]
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; } 

    }
}
