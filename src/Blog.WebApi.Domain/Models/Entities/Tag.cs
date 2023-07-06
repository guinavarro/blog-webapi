namespace Blog.WebApi.Domain.Models.Entities
{
    public class Tag : Base
    {
        public string Name { get; private set; }
        public ICollection<TagsPost> TagsPost { get; private set; }


        public Tag(string name)
        {
            UpdateName(name);
        }
        public Tag(string name, Guid key): base(key)
        {
            UpdateName(name);
        }


        void UpdateName(string name) => Name = name.ToLower().Trim();
    }
}
