using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalkAPI.Models.DTOs
{
    public class ImageAddUpdateDTO
    {
        public Guid Id { get; set; }
        public IFormFile FormFile { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string Path { get; set; }
    }
}
