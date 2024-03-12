using EFCore.Data.Enums;
using EFCore.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace EFCore.MySQL.Models.DTO
{
    public class CustomerDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required, EmailAddress(ErrorMessage = "Enter a valid Email Address")]
        public string Email { get; set; }

        [Required, Phone(ErrorMessage = "Enter a valid Mobile Number")]
        public string MobileNumber { get; set; }
        public string Facebook { get; set; }
        public Gender Gender { get; set; }
        public string Country { get; set; }
        public string Admin { get; set; }
        public string Comments { get; set; }
        //public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    }

}
