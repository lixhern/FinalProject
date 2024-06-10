using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FinalProject.ViewModels;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminPanelController : Controller
    {
        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;


        public AdminPanelController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AdminPanelModel model = new AdminPanelModel();
            model.Users = await userManager.Users.ToListAsync();
            model.UserManager = userManager;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TakeAwayAdminRights(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "admin");
            if (user.UserName  == User.Identity.Name) 
            {
                await signInManager.SignOutAsync();
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Account");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> GiveAdministatorRigths(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            await userManager.AddToRoleAsync(user, "admin");
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Block(string id)
        {
            if(await IsActiveAsync())
            {
                if (id != null)
                {
                    User user = await userManager.FindByIdAsync(id);
                    user.LockoutEnd = DateTime.Now.AddYears(200);
                    user.IsBlock = true;
                    await userManager.UpdateAsync(user);
                }
            }
            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Unblock(string id)
        {
            if(await IsActiveAsync())
            {
                if (id != null)
                {
                    User user = await userManager.FindByIdAsync(id);
                    user.IsBlock = false;
                    user.LockoutEnd = null;
                    await userManager.UpdateAsync(user);
                }
            }  
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (await IsActiveAsync())
            {
                if (id != null)
                {
                    User user = await userManager.FindByIdAsync(id);
                    var collections = context.Collections
                        .Where(c => c.UserId == user.Id)
                        .ToList();
                    foreach(var collection in collections)
                    {
                        DeleteCollection(collection);
                    }
                    
                    await userManager.DeleteAsync(user);
                }
            }
            
            return RedirectToAction("Index");
        }


        public async void DeleteCollection(Collection collection)
        {
            context.Collections.Remove(collection);
            await context.SaveChangesAsync();
        }


        public async Task<bool> IsActiveAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if ((User.Identity.IsAuthenticated && user == null) || (user.IsBlock))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
