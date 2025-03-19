using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.DB;
using NZWalkAPI.Models;
using NZWalkAPI.Repository.IRepository;
using System.Transactions;

namespace NZWalkAPI.Repository
{
    public class RegionService : IRegions
    {
        private readonly AppDBContext _db;
        public RegionService(AppDBContext db)
        {
            _db = db;
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
            var regions = await _db.Regions.ToListAsync();
            return regions;
        }

        public async Task<Region?> GetRegionById(Guid id)
        {
            return await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> AddRegion(Region region)
        {
            using var transaction = _db.Database.BeginTransaction();
            await _db.Regions.AddAsync(region);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                await transaction.CommitAsync();
                return region;
            }
            else
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<Region?> UpdateRegion(Guid id, Region region)
        {
            var regionData = await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (regionData == null)
            {
                return null;
            }
            regionData.Name = region.Name;
            regionData.Code = region.Code;
            regionData.RegionImageUrl = region.RegionImageUrl;
            using var transaction = _db.Database.BeginTransaction();
            _db.Regions.Update(regionData);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                await transaction.CommitAsync();
                return regionData;
            }
            else
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<Region?> DeleteRegion(Guid id)
        {
            var region = await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
            {
                return null;
            }
            using var transaction = _db.Database.BeginTransaction();
            _db.Regions.Remove(region);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                await transaction.CommitAsync();
                return region;
            }
            else
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

    }
}
