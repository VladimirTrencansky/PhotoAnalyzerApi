using Microsoft.AspNetCore.Mvc;
using PhotoAnalyzerApi.Objects;

namespace PhotoAnalyzerApi.Service
{
    public class PhotoAnalyzer : IPhotoAnalyzer
    {
        private readonly IPhotoProcessor _photoProcessor;

        public PhotoAnalyzer(IPhotoProcessor photoProcessor)
        {
            _photoProcessor = photoProcessor;
        }

        public IEnumerable<PhotoData> SaveAndAnalyzePhotos(FileModel files)
        {
            SavePhotos(files);

            var data = _photoProcessor.AnalyzePhotos();

            foreach (var photo in data)
            {
                photo.ImageBase64 = Convert.ToBase64String(File.ReadAllBytes(photo.Path));
            }

            return data;
        }

        private void SavePhotos(FileModel files)
        {
            var tempFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            try
            {
                ClearPhotoDir(tempFolder);

                foreach (var file in files.FormFile)
                {
                    string path = Path.Combine(tempFolder, file.FileName);

                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exceptions
            }
        }

        private void ClearPhotoDir(string tempFolder)
        {
            DirectoryInfo tempDir = new DirectoryInfo(tempFolder);
            foreach (FileInfo file in tempDir.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
