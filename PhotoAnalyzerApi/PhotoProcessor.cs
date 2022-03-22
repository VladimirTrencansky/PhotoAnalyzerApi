using ImageMagick;
using PhotoAnalyzerApi.Objects;

namespace PhotoAnalyzerApi
{
    public class PhotoProcessor : IPhotoProcessor
    {
        public IEnumerable<PhotoData> AnalyzePhotos()
        {
            List<PhotoData> result = new List<PhotoData>();

            var files = Directory.EnumerateFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));

            foreach (var file in files)
            {
                if (file.IsImage())
                {
                    result.Add(GetPhotoData(file));
                }
            }

            return result;
        }

        private PhotoData GetPhotoData(string photoPath)
        {
            PhotoData result = new PhotoData();

            try
            {
                using (var image = new MagickImage(photoPath))
                {
                    var profile = image.GetExifProfile();

                    if (profile != null)
                    {

                        result.Path = photoPath;
                        result.FocalLength = profile.GetValue(ExifTag.FocalLength)?.Value.ToDouble();
                        result.Width = (int)profile.GetValue(ExifTag.ImageWidth).Value;
                        result.Height = (int)profile.GetValue(ExifTag.ImageLength).Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.Log(LogLevel.Error, ex.Message);
            }

            return result;
        }

    }
}
