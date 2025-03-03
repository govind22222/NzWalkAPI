using WebAPI_VersioningNugetApproach.Model;

namespace WebAPI_VersioningNugetApproach.Data
{
    public class CountryData
    {
        public static List<Country> GetCountry()
        {

            List<Country> countries = new List<Country>()
            {
                new Country(){Id=1, Name="India"},
                new Country(){Id=1, Name="USA"},
                new Country(){Id=1, Name="Austrilia"},
                new Country(){Id=1, Name="UAE"},
            };
            return countries;
        }
    }
}
