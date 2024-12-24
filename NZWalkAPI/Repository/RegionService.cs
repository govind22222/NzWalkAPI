using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.DB;
using NZWalkAPI.Models;
using NZWalkAPI.Repository.IRepository;

namespace NZWalkAPI.Repository
{
    public class RegionService :IRegions
    {
        private readonly AppDBContext _db;
        public RegionService(AppDBContext db) 
        {
            _db = db;
        }

        public async Task<List<Region>> GetRegionsAsync() 
        {
            var regions= await _db.Regions.ToListAsync();
            //if (regions!=null)
            //{
            //    return regions;
            //}
            return regions;
        }



    }
}
