using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;


namespace FinalProject.Services
{
    public class JiraService
    {
        private readonly HttpClient HttpClient;
        private readonly string ProjectKey;

        public JiraService(IConfiguration configuration)
        {
            var jiraConfiguration = configuration.GetSection("Jira");
            ProjectKey = jiraConfiguration["ProjectKey"];
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(jiraConfiguration["BaseUrl"])
            };
            var email = jiraConfiguration["Email"];
            var apiToken = jiraConfiguration["ApiToken"];
            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{apiToken}"));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        }

        private async Task<string> CreateUserAsync(string email, string displayName)
        {
            var user = new
            {
                emailAddress = email,
                displayName = displayName,
                name = email.Split('@')[0],
                products = new[] { "jira-software" }
            };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync("/rest/api/3/user", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
            return jsonResponse.accountId;
        }


        public async Task<string> CreateIssueAsync(string summary, string description, string priority, string reporterEmail, string reporterName, string collectionName, string pageUrl)
        {
            var userId = await GetUserIdByEmail(reporterEmail);
            if (userId == null)
            {
                userId = await CreateUserAsync(reporterEmail, reporterName);
            }
            var issue = new
            {
                fields = new
                {
                    project = new { key = ProjectKey },
                    summary = summary,
                    description = description,
                    issuetype = new { name = "Task" },
                    reporter = new { id = userId},
                    customfield_10037 = new { value = priority },
                    customfield_10033 = collectionName,
                    customfield_10034 = pageUrl,
                    customfield_10036 = reporterEmail

                }
            };

            var jsonIssue = JsonConvert.SerializeObject(issue);
            var content = new StringContent(JsonConvert.SerializeObject(issue), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync("/rest/api/2/issue", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
            return $"{HttpClient.BaseAddress}browse/{jsonResponse.key}";
        }
        public async Task<dynamic> FindAllIssuesByEmailAsync(string email)
        {
            var response = await HttpClient.GetAsync($"/rest/api/3/search?jql=project={ProjectKey} AND reporter =\"" + email + "\"");
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
            return jsonResponse;
        }

        private async Task<string> GetUserIdByEmail(string email)
        {
            var response = await HttpClient.GetAsync($"/rest/api/3/user/search?query={email}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
            if (jsonResponse.Count == 0)
            {
                return null;
            }
            return jsonResponse[0].accountId;
        }

    }
}
