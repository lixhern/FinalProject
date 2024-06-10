using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FinalProject.Hubs
{
    public class LikeHub : Hub
    {
        public async Task Like(int itemId, int likesCount)
        {
            await Clients.All.SendAsync("ReceiveLikesUpdate", itemId, likesCount);
        }

    }
}
