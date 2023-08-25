namespace Blog.WebApi.Domain.Models.Entities
{
    public class Author : Base
    {
        public string Name { get; private set; }

        public Author(string name) => Name = name;
        
    }
}
