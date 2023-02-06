using Microsoft.AspNetCore.Identity;

namespace AceJobAgency.Model
{
    public class AceJobUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NRIC { get; set; }
        public string Gender { get; set; }
        public string DateofBirth { get; set; }
        public string Resume { get; set; }
        public string WhoamI { get; set; }
    }


}

