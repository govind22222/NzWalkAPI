using Microsoft.AspNetCore.Mvc;
using NZWalksWebApp.Models.DTO;

namespace NZWalksWebApp.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public RegionsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> APIIndex()
        {
            List<RegionsDto> regionsDto = new();
            try
            {
                
                //Getting all regions from the NZWalkAPI projects regions controller.
                var client = _httpClient.CreateClient(); // Creates a httpclient
                var httpResponse = await client.GetAsync("https://localhost:7059/api/regions");  //Hits the api and returns the response.
                httpResponse.EnsureSuccessStatusCode(); //Ensures the API response status core in 200, if not it throws exception.
                //var response = httpResponse.Content.ReadAsStringAsync();
                 regionsDto.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<RegionsDto>>());
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View(regionsDto);
        }
    }
}
