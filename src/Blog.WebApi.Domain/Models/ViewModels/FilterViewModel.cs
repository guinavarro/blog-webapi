namespace Blog.WebApi.Domain.Models.ViewModels
{
    public class FilterViewModel
    {
        public DateTime? PublishDate { get; set; }
        public string? ContainsInName { get; set; }
        public string? ContainsInText { get; set; }
        public List<string>? Tags { get; set; }
        public bool? Status { get; set; }
        public bool? WithImage { get; set; }
    }
}
