namespace Blog.WebApi.Domain.Models.Entities
{
    public class User : Base
    {
        public string UserName { get; set; }
        public string Email  { get; private set; }
        public string PasswordHash { get; private set; }

        public User(string userName, string email, string passwordHash)
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
