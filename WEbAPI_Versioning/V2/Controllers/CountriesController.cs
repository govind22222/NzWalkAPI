using Microsoft.AspNetCore.Mvc;
using WEbAPI_Versioning.Data;
using WEbAPI_Versioning.Model.DTO;

namespace WEbAPI_Versioning.V2.Controllers
{
    [ApiController]
    [Route("api/v2/controller")]
    public class CountriesController : ControllerBase
    {
        private readonly CountryData _country;

        //public WeatherForecastController( CountryData country) 
        //{
        //    _country = country;
        //}

        [HttpGet]
        public IActionResult GetCountry()
        {
            var countryList = CountryData.GetCountry();

            var countryDTO = new List<CountryDTOV1>();
            foreach (var country in countryList)
            {
                countryDTO.Add(new CountryDTOV1
                {
                    Id = country.Id,
                    Name = country.Name
                });
            }
            return Ok(countryDTO);
        }
    }
}
