using Blog.WebApi.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostViewModel model)
        {
            return Ok();
        }
    }
}
