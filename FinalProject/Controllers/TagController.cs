using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly ApplicationContext context;

        public TagController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags(string query)
        {
            var tags = await context.Tags
                .Where(t => t.Name.StartsWith(query))
                .ToListAsync();
            return Ok(tags);
        }
    }
}
