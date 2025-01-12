using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.DB;
using NZWalkAPI.Models;
using NZWalkAPI.Repository.IRepository;

namespace NZWalkAPI.Repository
{
    public class ImageService : IImage
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDBContext _db;
        public ImageService(IWebHostEnvironment webHost, IHttpContextAccessor httpContext, AppDBContext db)
        {
            _webHost = webHost;
            _httpContext = httpContext;
            _db = db;
        }

        public async Task<Image> UploadFile(Image img)
        {
            var filePath = Path.Combine(_webHost.ContentRootPath, @"Uploads\Images", $"{img.Name}{img.Extension}");
            using var filestream = new FileStream(filePath, FileMode.Create);
            await img.FormFile.CopyToAsync(filestream);

            //Providing custom dynamic location of file.
            var urlFilePath = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/{_httpContext.HttpContext.Request.PathBase}Uploads/Images/{img.Name}{img.Extension}";
            img.Path = urlFilePath;

            //Inserting the image path to the db.
            using var transaction = await _db.Database.BeginTransactionAsync();
            await _db.Images.AddAsync(img);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                await transaction.CommitAsync();
                return img;
            }
            else
            {
                await transaction.RollbackAsync();
                return null;
            }
        }
    }
}
