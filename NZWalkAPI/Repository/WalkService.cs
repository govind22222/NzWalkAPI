using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
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

        public async Task<List<Walk>> GetAllWalksAsync(string? filterBy = null, string? filterQuery = null, bool isAsc = true)
        {
            //var walk = await _db.Walks.Include(w => w.Difficulty).Include(w => w.Region).ToListAsync();
            var walk = _db.Walks.Include(w => w.Difficulty).Include(w => w.Region).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterBy) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                var filterCol = filterBy.ToString().ToLower();
                switch (filterCol)
                {
                    case "name":
                        walk = walk.Where(w => w.Name.Contains(filterCol, StringComparison.OrdinalIgnoreCase));
                        walk = isAsc == true ? walk.OrderBy(w => w.Name) : walk.OrderByDescending(w => w.Name);
                        break;
                    case "description":
                        walk = walk.Where(w => w.Description.Contains(filterCol, StringComparison.OrdinalIgnoreCase));
                        walk = isAsc == true ? walk.OrderBy(w => w.Description) : walk.OrderByDescending(w => w.Description);
                        break;
                    case "length":
                        walk = walk.Where(w => w.LengthInKm.ToString().Contains(filterCol, StringComparison.OrdinalIgnoreCase));
                        walk = isAsc == true ? walk.OrderBy(w => w.LengthInKm) : walk.OrderByDescending(w => w.LengthInKm);
                        break;
                    case "image":
                        walk = walk.Where(w => w.WalkImageUrl.Contains(filterCol, StringComparison.OrdinalIgnoreCase));
                        walk = isAsc == true ? walk.OrderBy(w => w.WalkImageUrl) : walk.OrderByDescending(w => w.WalkImageUrl);
                        break;
                    case "difficultyid":
                        walk = walk.Where(w => w.Difficulty.Id.ToString().Contains(filterCol, StringComparison.OrdinalIgnoreCase));
                        walk = isAsc == true ? walk.OrderBy(w => w.Difficulty.Id) : walk.OrderByDescending(w => w.Difficulty.Id);
                        break;
                    case "regionid":
                        walk = walk.Where(w => w.Region.Id.ToString().Contains(filterCol, StringComparison.OrdinalIgnoreCase));
                        walk = isAsc == true ? walk.OrderBy(w => w.Region.Id) : walk.OrderByDescending(w => w.Region.Id);
                        break;
                    default:
                        walk = walk.Where(w => w.Id.ToString().Contains(filterCol, StringComparison.OrdinalIgnoreCase));
                        break;
                        //walk = isAsc == true ? walk.OrderBy(w => w.Name) : walk.OrderByDescending(w => w.Name);
                }
            }
            return await walk.ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid walkId)
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
