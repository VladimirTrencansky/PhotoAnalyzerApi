using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using PhotoAnalyzerApi.Objects;
using PhotoAnalyzerApi.Service;
using System.Net;
using System.Text;

namespace PhotoAnalyzerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoAnalyzerController : ControllerBase
    {
        private readonly IPhotoAnalyzer _photoAnalyzerService;

        public PhotoAnalyzerController(IPhotoAnalyzer photoAnalyzerService)
        {
            _photoAnalyzerService = photoAnalyzerService;
        }

        [HttpPost]
        public IActionResult Post([FromForm] FileModel files)
        {
            var result = _photoAnalyzerService.SaveAndAnalyzePhotos(files);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("Export")]
        //public async Task<ActionResult> Get()
        //{
        //    var data = _photoAnalyzerService.AnalyzePhotos();

        //    var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var streamWriter = new StreamWriter(stream: memoryStream, encoding: new UTF8Encoding(true)))
        //        {
        //            using (var writer = new CsvWriter(streamWriter, csvConfig))
        //            {
        //                writer.WriteRecords(data);
        //            }
        //            return File(memoryStream.ToArray(), "text/csv", $"Export.csv");
        //        }
        //    }
        //}
    }
}
