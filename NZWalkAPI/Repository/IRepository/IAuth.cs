using Microsoft.AspNetCore.Identity;

namespace NZWalkAPI.Repository.IRepository
{
    public interface IAuth
    {
        public string CreateJwtToken(IdentityUser identityUser, List<string> roles);
    }
}
