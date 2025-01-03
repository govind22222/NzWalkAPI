using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTOs
{
    public class AddUpdateRegionDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Region length should be exceed 100 Chars.")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code should be min length of 3 Char.")]
        [MaxLength(4, ErrorMessage = "Code length should not be more than 4 Char.")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
