using NZWalkAPI.Models;

namespace NZWalkAPI.Repository.IRepository
{
    public interface IRegions
    {
        Task<List<Region>> GetRegionsAsync();
        Task<Region?> GetRegionById(Guid id);
        Task<Region?> AddRegion(Region region);
        Task<Region?> UpdateRegion(Guid id, Region region);
        Task<Region?> DeleteRegion(Guid id);
    }
}
