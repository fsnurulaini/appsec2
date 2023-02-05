using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AceJobAgency.Model;
using Microsoft.AspNetCore.DataProtection;
using AceJobAgency.Model;
using AceJobAgency.ViewModels;

namespace AceJobAgency.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<AceJobUser> userManager { get; }
        private SignInManager<AceJobUser> SignInManager { get; }

        private readonly AuthDbContext _context;

        [BindProperty]
        public Register RModel { get; set; }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        private IWebHostEnvironment _environment;


        public RegisterModel(UserManager<AceJobUser> userManager,
        SignInManager<AceJobUser> signInManager, IWebHostEnvironment environment, AuthDbContext context)
        {
            this.userManager = userManager;
            this.SignInManager = signInManager;
            _environment = environment;
            _context = context;
        }



        public void OnGet()
        {
        }



        public async Task<IActionResult> OnPostAsync(/*string gRecaptchaResponse*/)
        {
            if (EmailExists(RModel.Email))
            {
                ModelState.AddModelError("Email", "Email address already exists.");
                return Page();
            }
            /*if (!await IsRecaptchaPassed(gRecaptchaResponse))*/
            /*{
                ModelState.AddModelError(string.Empty, "The reCAPTCHA failed, try again.");
                return Page();
            }*/
            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("SecretKey");
                var user = new AceJobUser()
                {
                    UserName = RModel.Email,
                    PasswordHash = RModel.Password,
                    FirstName = RModel.FirstName,
                    LastName = RModel.LastName,
                    Email = RModel.Email,
                    Gender = RModel.Gender,
                    DateofBirth = RModel.DateofBirth,
                    Resume = RModel.Resume,
                    WhoamI = RModel.WhoamI,
                    NRIC = protector.Protect(RModel.NRIC)

                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, true);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();

            }
            return Page();
        }
        private bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
        /*private async Task<bool> IsRecaptchaPassed(string gRecaptchaResponse)
        {
            // Send a request to the Google reCAPTCHA API with the token and the secret key
            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_configuration["GoogleRecaptcha:SecretKey"]}&response={gRecaptchaResponse}");
            var response = JsonConvert.DeserializeObject<RecaptchaResponse>(result);

            // Check the response
            return response.Success && response.Score >= 0.5;
        }

        private class RecaptchaResponse
        {
            public bool Success { get; set; }

            [JsonProperty("score")]
            public double Score { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }

*/



    }
}
