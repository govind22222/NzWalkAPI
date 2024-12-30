using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using NZWalkAPI.DB;
using NZWalkAPI.Models;
using NZWalkAPI.Repository.IRepository;

namespace NZWalkAPI.Repository
{
    public class WalkService : IWalk
    {
        private readonly AppDBContext _db;

        public WalkService(AppDBContext db)
        {
            _db = db;
        }

        public async Task<Walk?> AddWalk(Walk walk)
        {
            using var transaction = _db.Database.BeginTransaction();
            await _db.Walks.AddAsync(walk);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                await transaction.CommitAsync();
                return walk;
            }
            else
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

    }
}
