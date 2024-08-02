using Microsoft.EntityFrameworkCore;
using NZWalkAPI.Models;
using System.Collections.Generic;

namespace NZWalkAPI.DB
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
