using Microsoft.AspNetCore.Identity;

namespace ToDo.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
