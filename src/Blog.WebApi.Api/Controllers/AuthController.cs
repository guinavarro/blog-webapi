using Blog.WebApi.Api.Models;
using Blog.WebApi.Domain.Interfaces;
using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.WebApi.Api.Controllers
{
    [ApiController]
    [Route("authorize")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthController(IConfiguration configuration,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(CreateUserDto request)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User(request.UserName, request.Email, passwordHash);

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return Ok("Usuário criado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto request)
        {
            var user = await _userRepository.FindUserByEmail(request.Email);

            // TODO: validar se usuário existe

            if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Senha errada");
            }
            
            var token = _tokenService.GenerateToken(user);

            return Ok(token);
        }



    }
}