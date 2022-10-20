namespace DotnetCoreKampı.Models
{
    public class ProfilePictureModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? WriterAbout { get; set; }
        public IFormFile? WriterImage { get; set; }
        public string? MailAdress { get; set; }
    }
}
