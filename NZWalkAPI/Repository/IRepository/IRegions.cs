using NZWalkAPI.Models;

namespace NZWalkAPI.Repository.IRepository
{
    public interface IRegions
    {
        Task<List<Region>> GetRegionsAsync();
    }
}
