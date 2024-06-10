using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FinalProject.ViewModels;
using FinalProject.Enum;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    
    public class CollectionController : Controller
    {
        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;
        

        public CollectionController(ApplicationContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            
        }


        public async Task<IActionResult> Index(int id)
        {
            var Collection = context.Collections
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == id);
            var items = context.Items
                .Where(i => i.CollectionId == id)
                .ToList();

            CreateItemModel cim = new CreateItemModel(items, Collection);
            return View(cim);
        }

        

        [Authorize]
        public IActionResult Create() 
        {
            return View();
        }

        public async Task<IActionResult> CollectionsByCategory(Category category)
        {
            CollectionsCategory cs = new CollectionsCategory();
            cs.Collections = context.Collections
                .Include(c => c.User)
                .Where(c => c.Category == category)
                .ToList();
            cs.CategoryName = category;
            return View(cs);
        }
        public async Task<IActionResult> Collections()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var collections = context.Collections
                .Where(c => c.UserId == user.Id)
                .ToList();
            return View(collections);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCollectionViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                Collection collection = new Collection { 
                    Name = model.Name, 
                    Description = model.Description, 
                    UserId = user.Id, 
                    Category = model.Category,
                    CustomString1State = model.CustomString1State,
                    CustomString1Name = model.CustomString1State ? model.CustomString1Name : string.Empty,
                    CustomString2State = model.CustomString2State,
                    CustomString2Name = model.CustomString2State ? model.CustomString2Name : string.Empty,
                    CustomString3State = model.CustomString3State,
                    CustomString3Name = model.CustomString3State ? model.CustomString3Name : string.Empty,
                };
                context.Collections.Add(collection);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Account");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id, string extraParam)
        {

            var collection = await context.Collections.FindAsync(id);
            context.Collections.Remove(collection);
            await context.SaveChangesAsync();
            return RedirectToAction("Collections", extraParam);
        }
    }
}
