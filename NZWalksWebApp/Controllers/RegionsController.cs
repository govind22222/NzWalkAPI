using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksWebApp.Models.DTO;
using NZWalksWebApp.Models.ViewModel;

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


        public async Task<IActionResult> AddRegion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionUsingAPI([FromBody] RegionViewModel regionModel)
        {
            if (regionModel != null)
            {
                var client = _httpClient.CreateClient();
                var httpReqMessage = new HttpRequestMessage()
                {
                  Method=HttpMethod.Post,
                  RequestUri= new Uri("https://localhost:7059/api/regions"),
                  Content= new StringContent(JsonSerializer.Serialize(regionModel), Encoding.UTF8, "application/json")
                };

                var httpResponseMsg = await client.SendAsync(httpReqMessage);
                httpResponseMsg.EnsureSuccessStatusCode(); //Ensures the API response status core in 200, if not it throws exception.
                var response= await httpResponseMsg.Content.ReadFromJsonAsync<RegionsDto>();
                if (response != null && httpResponseMsg.IsSuccessStatusCode)
                {
                    return Json(new { isSuccess = true, responseData=response, message = "Region Added." });
                }
            }
            else
            {
                return Json(new { isSuccess = false,  message = "Region not Added." });
            }
            return Ok();
        }
    }
}
