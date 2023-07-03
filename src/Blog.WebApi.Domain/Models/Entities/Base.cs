namespace Blog.WebApi.Domain.Models.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
