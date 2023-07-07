using Blog.WebApi.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Blog.WebApi.Domain.Services
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken()
        {
            throw new NotImplementedException();
        }
    }
}
