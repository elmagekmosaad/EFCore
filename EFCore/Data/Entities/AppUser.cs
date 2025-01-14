using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Web.Api.Data.Entities.Enums;

namespace EFCore.Data.Models
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Name { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        //public string PhoneNumber { get; set; }
        public string Facebook { get; set; }
        public CustomerType? Type { get; set; }
        public CustomerGender? Gender { get; set; }
        public string Country { get; set; }
        public string Admin { get; set; }
        public string Comments { get; set; }
        public virtual ICollection<Subscription>? Subscriptions { get; set; }
    }
}