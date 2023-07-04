using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.WebApi.Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost("token")]

        // Criar o método de login, se email e senha estiver correto, criar um token para o usuario

        public IActionResult GenerateToken([FromBody] string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("tokenSecret");
            var 

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, "teste"),
                new(JwtRegisteredClaimNames.Email, "teste")
            };



        }
       

        //private static string GenerateToken()
        //{

        //}
    }
}