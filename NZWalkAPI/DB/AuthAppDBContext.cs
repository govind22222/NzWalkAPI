using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalkAPI.DB
{
    public class AuthAppDBContext :IdentityDbContext
    {
        public AuthAppDBContext( DbContextOptions<AuthAppDBContext> options): base(options)
        {
            
        }
    }
}
