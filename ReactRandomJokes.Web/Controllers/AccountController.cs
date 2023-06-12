using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ReactRandomJokes.Data;
using ReactRandomJokes.Web.Models;
using System.Security.Claims;

namespace ReactRandomJokes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private string _connectionString;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("signup")]
        public void Signup(SignupViewModel user)
        {
            var repo = new UserRepository(_connectionString);
            repo.AddUser(user, user.Password);
        }

        [HttpPost]
        [Route("login")]
        public User Login(LoginViewModel lvm)
        {
            var repo = new UserRepository(_connectionString);
            var user = repo.Login(lvm.Email, lvm.Password);
            if (user == null)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim("user", lvm.Email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return user;
        }
        [HttpGet]
        [Route("getcurrentuser")]
        public User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            var repo = new UserRepository(_connectionString);
            var user = repo.GetByEmail(User.Identity.Name);
            return user;
        }

        [HttpPost]
        [Route("logout")]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }
    }
}
