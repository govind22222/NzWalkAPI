using WEbAPI_Versioning.Model;

namespace WEbAPI_Versioning.Data
{
    public class CountryData
    {
        public static List<Country> GetCountry() {
            var country = new List<Country>() 
            {
                new Country(){ Id = 1, Name = "IND" },
                new Country(){ Id = 2, Name = "UAE" },
                new Country(){ Id = 3, Name = "USA" },
                new Country(){ Id = 4, Name = "Africa" },
                new Country(){ Id = 5, Name = "Atlanta" },
                new Country(){ Id = 6, Name = "Russia" },
                new Country(){ Id = 7, Name = "Ukraine" },
                new Country(){ Id = 8, Name = "USA" },
            };

            return country;
        }

    }
}
