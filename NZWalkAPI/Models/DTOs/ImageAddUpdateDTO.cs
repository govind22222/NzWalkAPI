using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalkAPI.Models.DTOs
{
    public class ImageAddUpdateDTO
    {
        //public Guid Id { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        //public string Extension { get; set; }
        //public long FileSizeInBytes { get; set; }
        //public string Path { get; set; }
    }
}
