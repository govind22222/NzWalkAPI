using Microsoft.AspNetCore.Mvc;
using WEbAPI_Versioning.Data;
using WEbAPI_Versioning.Model.DTO;

namespace WEbAPI_Versioning.V1.Controllers
{
    [ApiController]
    [Route("api/v1/controller")]
    public class CountriesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetCountry()
        {
            var countryList = CountryData.GetCountry();

            var countryDTO = new List<CountryDTOV2>();
            foreach (var country in countryList)
            {
                countryDTO.Add(new CountryDTOV2
                {
                    Id = country.Id,
                    CountryName = country.Name
                });
            }
            return Ok(countryDTO);
        }
    }
}
