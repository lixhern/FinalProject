using Microsoft.AspNetCore.Identity;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Middleware
{
    public class IsActiveUserMiddleware
    {
        private readonly RequestDelegate next;

        public IsActiveUserMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userManager = context.RequestServices.GetService<UserManager<User>>();
            var signInManger = context.RequestServices.GetService<SignInManager<User>>();
            var user = await userManager.GetUserAsync(context.User);

            if(user == null && context.User.Identity.IsAuthenticated) 
            {
                await signInManger.SignOutAsync();
                context.Response.Redirect("/Account/Login");
            }
            if(user != null && user.IsBlock) 
            {
                await signInManger.SignOutAsync();
                context.Response.Redirect("/Account/Login");
            }
            await next(context);
        }
    }
}
