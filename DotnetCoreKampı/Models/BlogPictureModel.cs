namespace DotnetCoreKampı.Models
{
    public class BlogPictureModel
    {
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
        public  IFormFile? BlogThumbnailImage { get; set; }
        public IFormFile? BlogImage { get; set; }
        public int CategoryID { get; set; }
    }
}
