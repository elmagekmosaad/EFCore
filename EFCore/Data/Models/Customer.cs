using EFCore.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace EFCore.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Facebook { get; set; }
        public Gender Gender { get; set; }
        public string Country { get; set; }
        public string Admin { get; set; }
        public string Comments { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    }
}
