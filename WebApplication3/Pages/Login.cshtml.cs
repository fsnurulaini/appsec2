using AceJobAgency.Model;
using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AceJobAgency.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<AceJobUser> signInManager;
        private readonly AuthDbContext _context;
        private readonly ILogger<LoginModel> _logger;


        public LoginModel(SignInManager<AceJobUser> signInManager, ILogger<LoginModel> logger)
        {
            this.signInManager = signInManager;
            _logger = logger;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, lockoutOnFailure: false);
                var user = await signInManager.UserManager.FindByNameAsync(LModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToPage("LoginIndex");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            return Page();
        }
        /*private bool EmailExists(string email)
        {
            return _context.Users.Any((u => u.Email == email));
        }
*/
    }
}

