namespace Blog.WebApi.Domain.Models.ViewModels
{
    public record PostViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public List<string>? Tags { get; set; }
        public ImageViewModel? Image { get; set; }
    }

    public record ImageViewModel
    {
        public string Name {  get; set; }
        public string DataBase64 { get; set; }
        public string ContentType { get; set; }
    }
}

