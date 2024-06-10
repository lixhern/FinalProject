using Microsoft.AspNetCore.SignalR;
using System.Globalization;
using System.Threading.Tasks;

namespace FinalProject.Hubs
{
    public class CommentHub : Hub
    {
        public async Task Comment(string commentText, string userName, int commentId, bool isAdmin, bool isOwner)
        {
            await Clients.All.SendAsync("ReceiveCommentUpdate", commentText, userName, commentId, isAdmin, isOwner);
        }
    }
}
