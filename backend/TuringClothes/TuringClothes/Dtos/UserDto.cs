using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;

namespace TuringClothes.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public string Role { get; set; }

        public ICollection<Order> orders { get; set; }
    }
}
