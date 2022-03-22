namespace PhotoAnalyzerApi.Objects
{
    public class PhotoData
    {
        public string Path { get; set; }
        public string Title => getTitle();

        public int Width { get; set; }
        public int Height { get; set; } 
        public double? FocalLength { get; set; }

        public string ImageBase64 { get; set; }



        private string getTitle()
        {
            string title = string.Empty;
            if (!string.IsNullOrEmpty(Path))
            {
                title = Path.Split('\\').LastOrDefault();
            }

            return title;
        }
    }
}
