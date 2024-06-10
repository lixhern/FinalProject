using FinalProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalProject.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class JiraController : Controller
{
    private readonly JiraService JiraService;
    private readonly UserManager<User> userManager;
    private readonly ApplicationContext context;

    public JiraController(JiraService JiraService, UserManager<User> userManager, ApplicationContext context)
    {
        this.JiraService = JiraService;
        this.userManager = userManager;
        this.context = context;
    }

    [HttpGet]
    public IActionResult CreateIssue(string currentUrl)
    {
        return View((object)currentUrl);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssue(string pageUrl, string summary, string description, string priority, string collectionName)
    {
        var collection = context.Collections.FirstOrDefault(c => c.Name == collectionName);
        if (collection != null || collectionName == null)
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            try
            {
                var result = await JiraService.CreateIssueAsync(summary, description, priority, user.Email, user.UserName, collectionName, pageUrl);
                ViewBag.IssueLink = result;
                return View("IssueCreated");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Something went wrong, please try later";
                return View("Error");
            }
        }
        else
        {
            ViewBag.Error = "There is no such collections";
            return View("Error");
        }
        

    }
    [HttpGet]
    public async Task<IActionResult> ShowIssues()
    {
        User user = await userManager.FindByNameAsync(User.Identity.Name);
        dynamic respone = await JiraService.FindAllIssuesByEmailAsync(user.Email);
        return View(respone);
    }
}
