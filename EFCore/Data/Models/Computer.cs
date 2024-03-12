using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Data.Models
{
    public class Computer
    {
        [Key]
        public int Id { get; set; }
        public string Hwid { get; set; }
        public string Serial { get; set; }

        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Subscription Subscription { get; set; }

        [ForeignKey(nameof(Subscription))]
        public int SubscriptionId { get; set; }
    }
}
