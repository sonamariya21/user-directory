using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Age { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
        public string Pincode { get; set; } 
    }
}
