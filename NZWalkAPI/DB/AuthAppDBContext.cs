using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalkAPI.DB
{
    public class AuthAppDBContext : IdentityDbContext
    {
        public AuthAppDBContext(DbContextOptions<AuthAppDBContext> options) : base(options)
        {
        }

        //Seeding Roles to DB by Raghav on 08-Jan-25.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var writeRole = "af962303-b468-423d-bdf4-7d8589e94687";
            var readRole = "b4e0b098-c394-495d-88e4-3420711577b9";
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id=writeRole,
                    Name="WriteRole",
                    NormalizedName= "WriteRole".ToUpper(),
                    ConcurrencyStamp =writeRole
                },
                new IdentityRole
                {
                    Id=readRole,
                    Name="ReadRole",
                    NormalizedName="ReadRole".ToUpper(),
                    ConcurrencyStamp=readRole
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
