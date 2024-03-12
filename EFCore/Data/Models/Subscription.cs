using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EFCore.Data.Enums;

namespace EFCore.Data.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public SubscriptionPeriod Period { get; set; }
        public DateTime StartOfSubscription { get; set; }
        public DateTime EndOfSubscription { get; set; }
        public int Points { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        [ForeignKey(nameof(Customer))]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(Computer))]
        public int? ComputerId { get; set; }
        public virtual Computer Computer { get; set; }

    }
}