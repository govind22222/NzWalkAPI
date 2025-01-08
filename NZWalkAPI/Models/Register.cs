using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Role {  get; set; }
    }
}
