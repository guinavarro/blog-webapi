using Blog.WebApi.Domain.Interfaces.Services;
using Blog.WebApi.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : Controller
    {

        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostViewModel model)
        {

            var result = await _blogService.Post(model);
            return Ok();
        }
    }
}
