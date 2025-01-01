using NZWalkAPI.Models;

namespace NZWalkAPI.Repository.IRepository
{
    public interface IWalk
    {
        Task<Walk?> AddWalkAsync(Walk region);
        Task<List<Walk>> GetAllWalksAsync();
        Task<Walk?> GetWalkByIdAsync(Guid id);
        //Task<Walk?> UpdateWalkAsync(Guid id, Walk Walk);
        //Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
