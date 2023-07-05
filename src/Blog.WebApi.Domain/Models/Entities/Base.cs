namespace Blog.WebApi.Domain.Models.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public DateTime Date { get; set; }

        public Base()
        {
            
        }
        public Base(Guid key) => Key = key;
        
    }
}
