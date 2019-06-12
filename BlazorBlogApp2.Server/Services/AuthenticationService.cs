using BlazorBlogApp2.Shared;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlazorBlogApp2.Server.Services
{
    public class AuthenticationService
    {
        private readonly ToDoListContext _context;

        public AuthenticationService()
        {
            _context = new ToDoListContext();
        }

        public async Task<int> Login(Account loginDetails)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Username.Equals(loginDetails.Username) && x.Password.Equals(loginDetails.Password));

            if (user == null) return 0;

            return user.UserId;
        }
    }
}
