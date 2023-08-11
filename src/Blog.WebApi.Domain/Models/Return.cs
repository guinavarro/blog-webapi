namespace Blog.WebApi.Domain.Models
{
    public class Return<T>
    {
        public Return(bool success, string message, T? entity = default)
        {
            Success = success;
            Message = message;
            Entity = entity;
        }

        public bool Success { get; protected set; }
        public string Message {  get; protected set; }
        public T? Entity { get; protected set; }

    }
}
