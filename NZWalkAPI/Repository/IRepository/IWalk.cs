using NZWalkAPI.Models;

namespace NZWalkAPI.Repository.IRepository
{
    public interface IWalk
    {
        //Task<List<Walk>> GetAllWalks();
        //Task<Walk?> GetWalkById(Guid id);
        Task<Walk?> AddWalk(Walk region);
        //Task<Walk?> UpdateWalk(Guid id, Walk Walk);
        //Task<Walk?> DeleteWalk(Guid id);
    }
}
