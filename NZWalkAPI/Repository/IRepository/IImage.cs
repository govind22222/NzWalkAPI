using NZWalkAPI.Models;

namespace NZWalkAPI.Repository.IRepository
{
    public interface IImage
    {
        public Task<Image> UploadFile(Image img);
    }
}
