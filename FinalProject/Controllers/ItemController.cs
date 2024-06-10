using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{

    public class ItemController : Controller
    {

        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;

        public ItemController(ApplicationContext context, UserManager<User> userManager)
        { 
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<ActionResult> Index(int id)
        {
            var item = await context.Items
                .Include(i => i.Collection)
                .Include(i => i.User)
                .Include(i => i.Likes)
                .Include(i => i.Tags)
                .Include(i => i.Comments)
                .Where(i => i.Id == id)
                .Select(i => new
                {
                    Item = i,
                    Comments = i.Comments,
                    LikesCount = i.Likes.Count()
                })
                .FirstOrDefaultAsync();

            
            ItemModel lk = new ItemModel
            {
                Item = item.Item,
                Comments = item.Comments.ToList(),
                LikesCount = item.LikesCount
            };
            return View(lk);
        }


        public async Task<IActionResult> ItemsByTag(string tagName)
        {
            ItemTagModel itm = new ItemTagModel();
            itm.Items = await context.Items
                .Include(i => i.User)
                .Include(i => i.Collection)
                .Where(i => i.Tags.Any(t => t.Name == tagName))
                .ToListAsync();
            itm.TagName = tagName;
            return View(itm);
        }
        [Authorize]
        public ActionResult Create(int id )
        {
            var Collection = context.Collections.FirstOrDefault(c => c.Id == id);
            ViewBag.Collection = Collection;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateItemViewModel model, int id) 
        {
            var Collection = context.Collections.FirstOrDefault(c => c.Id == id);
            ViewBag.Collection = Collection;
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                Item item = new Item
                {
                    Name = model.Name,
                    CustomString1 = model.CustomString1Name,
                    CustomString2 = model.CustomString2Name,
                    CustomString3 = model.CustomString3Name,
                    CollectionId = Collection.Id,
                    UserId = Collection.UserId,

                };
                context.Items.Add(item);
                string[] tags = model.Tags.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tagName in tags)
                {
                    var tag = context.Tags.FirstOrDefault(t => t.Name == tagName);
                    if (tag == null)
                    {
                        tag = new Tag { Name = tagName };
                        context.Tags.Add(tag);
                    }

                    item.Tags.Add(tag);
                }
                await context.SaveChangesAsync();
                return RedirectToAction("Index",  "Collection", new {id});
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await context.Items.FindAsync(id);
            id = item.CollectionId;
            if (item == null)
            {
                return NotFound();
            }
            context.Items.Remove(item);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Collection", new {id});
        }
    }
}
