using EFCore.Data.Models;
using System.ComponentModel.DataAnnotations;
using Web.Api.Data.Entities.Enums;

namespace EFCore.Models.Dtos.Auth
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Enter a valid Email Address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Phone(ErrorMessage = "Enter a valid Mobile Number")]
        public string PhoneNumber { get; set; }
        public string Facebook { get; set; }
        public CustomerGender Gender { get; set; }
        public string Country { get; set; }
        public string Admin { get; set; }
        public string Comments { get; set; }
        public virtual ICollection<Subscription>? Subscriptions { get; set; }
    }
}