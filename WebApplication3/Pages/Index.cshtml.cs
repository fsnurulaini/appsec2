using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AceJobAgency.Model;

namespace AceJobAgency.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<AceJobUser> _userManager;
        public IndexModel(UserManager<AceJobUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> GetCurrentUserId()
        {
            AceJobUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        private Task<AceJobUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public void OnGet()
        {

        }
    }
}