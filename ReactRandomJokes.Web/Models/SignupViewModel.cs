using Microsoft.AspNetCore.Identity;
using ReactRandomJokes.Data;

namespace ReactRandomJokes.Web.Models
{
    public class SignupViewModel : User
    {
        public string Password { get; set; }
    }
}
