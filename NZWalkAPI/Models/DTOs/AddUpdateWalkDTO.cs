using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTOs
{
    public class AddUpdateWalkDTO
    {
        [Required]
        [MaxLength(30, ErrorMessage ="Name should not exceed 30 chars.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Description should not exceed 100 chars.")]
        public string Description { get; set; }
        [Required]
        [Range(0,50, ErrorMessage ="Length should be between 0-50Km.")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //Added DifficultyId and RegionId for Relationship with Region and Difficulty Table.
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
