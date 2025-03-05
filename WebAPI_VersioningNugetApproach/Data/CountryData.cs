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
                new Country(){Id=2, Name="USA"},
                new Country(){Id=3, Name="Austrilia"},
                new Country(){Id=4, Name="UAE"},
            };
            return countries;
        }
    }
}
