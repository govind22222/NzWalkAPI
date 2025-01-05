using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models;

namespace NZWalkAPI.Repository.IRepository
{
    public interface IWalk
    {
        Task<Walk?> AddWalkAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync(string? filterBy = null, string? filterQuery = null, bool isAsc=true);
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk Walk);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
