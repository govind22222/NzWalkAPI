using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.DTOs;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageAddUpdateDTO imgDto)
        {
            ValidateFileUpload(imgDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        public void ValidateFileUpload(ImageAddUpdateDTO imgDto)
        {
            var allowedExten = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExten.Contains(Path.GetExtension(imgDto.FormFile.FileName)))
            {
                ModelState.AddModelError("FormFile", "Unsupported file format.");
            }
            if (imgDto.FormFile.Length > 10000000)
            {
                ModelState.AddModelError("FormFile", "File size is mote than 10MB, Please upload file max upto 10MB.");
            }
        }
    }
}
