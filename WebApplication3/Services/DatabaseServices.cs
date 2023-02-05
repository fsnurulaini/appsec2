using AceJobAgency.Model;
using Microsoft.EntityFrameworkCore;

namespace AceJobAgency.Services
{
    public class DatabaseServices
    {
        private readonly AuthDbContext _context;
        public DatabaseServices(AuthDbContext context)
        {
            _context = context;
        }
        public List<AceJobUser> GetAceJobUsers()
        {
            return _context.Users.OrderBy(m => m.FirstName).ToList();
        }

    }
}
