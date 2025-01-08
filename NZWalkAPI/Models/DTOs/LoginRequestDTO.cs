using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
