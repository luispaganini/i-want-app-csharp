namespace IWantApp.Controllers.Categories
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            return Ok();
        }

        [HttpPost]
        public async Task PostAsync([FromBody] string value)
        {
            
        }
    }
}        
