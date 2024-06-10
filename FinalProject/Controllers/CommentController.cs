using FinalProject.Hubs;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;
        private readonly IHubContext<CommentHub> hubContext;

        public CommentController(ApplicationContext context, UserManager<User> userManager, IHubContext<CommentHub> hubContext)
        {
            this.context = context;
            this.userManager = userManager;
            this.hubContext = hubContext;
        }


        public async Task<IActionResult> AddComment(int ItemId, string UserName, string Comment)
        {
             
            var item = await context.Items
                .Include(i => i.User)
                .Include(i => i.Comments)
                .FirstOrDefaultAsync(i => i.Id == ItemId);
            var user = await userManager.FindByNameAsync(UserName);
            var isAdmin = User.IsInRole("admin");
            var isOwner = User.Identity.Name.Equals(item.User.UserName);
            Comment comment = new Comment {ItemId =  ItemId, UserName = user.UserName, Text = Comment};

            context.Comments.Add(comment);
            item.Comments.Add(comment);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveCommentUpdate", comment.Text, UserName, comment.Id, isAdmin, isOwner);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var commentDb = await context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            context.Comments.Remove(commentDb);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveCommentDelete", commentId);
            return RedirectToAction("Index", "Home");
        }
    }
}
