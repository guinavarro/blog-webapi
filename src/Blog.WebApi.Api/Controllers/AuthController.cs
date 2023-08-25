using Blog.WebApi.Api.Models;
using Blog.WebApi.Domain.Interfaces;
using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Interfaces.Services;
using Blog.WebApi.Domain.Models.Entities;
using Blog.WebApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Api.Controllers
{
    [ApiController]
    [Route("authorize")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration,
            ITokenService tokenService,
            IUserService userService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(CreateUserDto request)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User(request.UserName, request.Email, passwordHash);

            var response = await _userService.Register(user);
            
            if (response.Success)
            {
                return Ok("Usuário criado com sucesso!");
            }
            else
            {
                return BadRequest("Houve um erro na hora de criar o Usuário");
            }            
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto request)
        {
            var user = await _userService.FindUserByEmail(request.Email);

            if(user == null)
            {
                return BadRequest("Usuário não encontrado para o e-mail passado");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Senha errada");
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(token);
        }



    }
}