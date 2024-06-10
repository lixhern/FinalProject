using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {


        private readonly ApplicationContext dbContext; 
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext context;


        public HomeController(ILogger<HomeController> logger, ApplicationContext dbContext, UserManager<User> userManager, ApplicationContext context)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            HomePanelModel homePanelModel = new HomePanelModel();

            homePanelModel.Collections = dbContext.Collections
                .Include(c => c.User)
                .OrderByDescending(c => c.Items.Count)
                .Take(5)
                .ToList();

            homePanelModel.Items = dbContext.Items
                .Include(item => item.User)
                .Include(item => item.Collection)
                .OrderByDescending(item => item.Id)
                .Take(5)
                .ToList();

            homePanelModel.Tags = dbContext.Tags
                .Include(t => t.Items)
                .OrderByDescending(t => t.Items.Count)
                .Take(30)
                .Select(t => new TagModel
                {
                    Name = t.Name,
                    Count = t.Items.Count,
                    Id = t.Id
                })
                .ToList();
            return View(homePanelModel);
        }

        [HttpGet]
        public async Task<IActionResult> Collections()
        {
            var collections = await context.Collections
                .Include(c => c.User)
                .ToListAsync();
            return View(collections);
        }

        public async Task<IActionResult> UserView(string id)
        {
            UserViewModel userViewModel = new UserViewModel();

            userViewModel.user = await userManager.FindByIdAsync(id);
            userViewModel.Collections = dbContext.Collections
                .Where(c => c.UserId == id)
                .ToList();
            userViewModel.Items = dbContext.Items
                .Where(i => i.UserId == id)
                .ToList();
            return View(userViewModel);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
