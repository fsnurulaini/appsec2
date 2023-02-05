using AceJobAgency.Services;
using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AceJobAgency.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DatabaseServices databaseServices;
        public IndexModel(DatabaseServices _databaseServices)
        {
            databaseServices = _databaseServices;
        }
        public List<Register> DetailList { get; set; } = new();

        public ViewModels.Register Details { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(AceJobUser aceJobUser)
        {
            var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
            var protector = dataProtectionProvider.CreateProtector("SecretKey");

        }
    }
}