using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebAPI_VersioningNugetApproach.Data;
using WebAPI_VersioningNugetApproach.DTOs;

namespace WebAPI_VersioningNugetApproach.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/countries")]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult GetCountryListV1()
        {
            var countryData = CountryData.GetCountry();
            var countryList = countryData.Select(item => new CountryDtoV1 { Id = item.Id, Name = item.Name }).ToList();
            return Ok(countryList);
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult GetCountryListV2()
        {
            var countryData = CountryData.GetCountry();
            var countryList = countryData.Select(item => new CountryDtoV2 { Id = item.Id, CountryName = item.Name }).ToList();
            return Ok(countryList);
        }
    }

}
