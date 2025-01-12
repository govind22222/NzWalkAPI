using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;
using NZWalkAPI.Repository.IRepository;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImage _image;
        private readonly IMapper _mapper;

        public ImagesController(IImage image, IMapper mapper)
        {
            _image = image;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageAddUpdateDTO imgDto)
        {
            ValidateFileUpload(imgDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imgModel = _mapper.Map<Image>(imgDto);
            imgModel.Extension = Path.GetExtension(imgDto.FormFile.FileName);
            imgModel.FileSizeInBytes = imgDto.FormFile.Length;

            //var imgDTO = _mapper.Map<ImageAddUpdateDTO>(await _image.UploadFile(imgModel));
            imgModel = await _image.UploadFile(imgModel);
            if (imgModel == null)
            {
                return BadRequest();
            }
            return Ok(imgModel);
        }


        private void ValidateFileUpload(ImageAddUpdateDTO imgDto)
        {
            var allowedExten = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExten.Contains(Path.GetExtension(imgDto.FormFile.FileName)))
            {
                ModelState.AddModelError("FormFile", "Unsupported file format.");
            }
            if (imgDto.FormFile.Length > 10000000)
            {
                ModelState.AddModelError("FormFile", "File size is mote than 10MB, Please upload file max up to 10MB.");
            }
        }
    }
}
