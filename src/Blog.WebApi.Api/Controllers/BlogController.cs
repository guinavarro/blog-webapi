using Blog.WebApi.Domain.Interfaces.Services;
using Blog.WebApi.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreatePostViewModel model)
        {
            var result = await _blogService.Post(model);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPostById([FromQuery] Guid key)
        {
            var result = await _blogService.GetPostById(key);

            if(result.Success)
                return Json(result.Entity);

            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] FilterViewModel filter)
        {
            var result = await _blogService.GetAllPosts(filter);

            if (result.Success)
                return Json(result.Entity);

          return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> DisablePost([FromQuery] Guid key)
        {
            var result = await _blogService.DisablePost(key);

            if (result.Success)
                return Json(result.Entity);

            return BadRequest(result.Message);
        }
    }
}
