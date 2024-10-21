using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models
{
    public class Difficulty
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
