using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalkAPI.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string Path { get; set; }
    }
}
