using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public int Year { get; set; }
    }
}
