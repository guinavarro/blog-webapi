using Blog.WebApi.Domain.Models.Entities;

namespace Blog.WebApi.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }

}
