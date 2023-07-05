namespace Blog.WebApi.Domain.Models.ViewModels
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public List<string>? Tags { get; set; }
        public byte[]? FileBytes { get; set; }
    }

}

