using FinalProject.Hubs;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class LikeController : Controller
    {
        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;
        private readonly IHubContext<LikeHub> hubContext;

        public LikeController(ApplicationContext context, UserManager<User> userManager, IHubContext<LikeHub> hubContext)
        {
            this.context = context;
            this.userManager = userManager;
            this.hubContext = hubContext;
        }

        public async Task<IActionResult> Like(int ItemId, string UserName)
        {
            var item = await context.Items
                .Include(i => i.Likes)
                .FirstOrDefaultAsync(i => i.Id == ItemId);
            var user = await userManager.FindByNameAsync(UserName);
            Like like = new Like { ItemId = item.Id, UserId = user.Id };

            var likeDb = await context.Likes.FirstOrDefaultAsync(l => l.UserId == user.Id && l.ItemId == ItemId);
            if (likeDb == null)
            {
                context.Likes.Add(like);
                item.Likes.Add(like);
            }
            else
            {
                context.Likes.Remove(likeDb);
                item.Likes.Remove(likeDb);
            }
            await context.SaveChangesAsync();
            int likesCount = await context.Likes.CountAsync(l => l.ItemId == ItemId);
            await hubContext.Clients.All.SendAsync("ReceiveLikesUpdate", ItemId, likesCount);
            return RedirectToAction("Index", "Home");
        }
    }
}