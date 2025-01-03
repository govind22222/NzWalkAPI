using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.DB;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;
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

        public async Task<Walk?> AddWalkAsync(Walk walk)
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

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            var walk = await _db.Walks.Include(w => w.Difficulty).Include(w => w.Region).ToListAsync();
            return walk;
        }

        public async Task<Walk> GetWalkByIdAsync(Guid walkId)
        {
            var walk = await _db.Walks.Include(w => w.Difficulty).Include(w => w.Region).FirstOrDefaultAsync(w => w.Id == walkId);
            if (walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid walkId, Walk walk)
        {
            var walkData = await _db.Walks.FirstOrDefaultAsync(w => w.Id == walkId);

            if (walkId != Guid.Empty && walk != null && walkData != null)
            {
                walkData.Name = walk.Name;
                walkData.Description = walk.Description;
                walkData.LengthInKm = walk.LengthInKm;
                walkData.WalkImageUrl = walk.WalkImageUrl;
                walkData.DifficultyId = walk.DifficultyId;
                walkData.RegionId = walk.RegionId;
                using var transaction = await _db.Database.BeginTransactionAsync();
                _db.Walks.Update(walkData);
                if (!Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    return null;
                }
                else
                {
                    await transaction.CommitAsync();
                    return walkData;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<Walk?> DeleteWalkAsync(Guid walkDeleteId)
        {
            var walkModel = await _db.Walks.FirstOrDefaultAsync(w => w.Id == walkDeleteId);
            if (walkModel == null)
            {
                return null;
            }
            using var transaction = await _db.Database.BeginTransactionAsync();
            _db.Walks.Remove(walkModel);
            if (!Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                await transaction.RollbackAsync();
                return null;
            }
            else
            {
                await transaction.CommitAsync();
                return walkModel;
            }
        }

    }
}
