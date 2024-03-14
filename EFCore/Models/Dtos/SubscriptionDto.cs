using EFCore.Data.Enums;

namespace EFCore.MySQL.Models.Dto
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public SubscriptionPeriod Period { get; set; }
        public DateTime StartOfSubscription { get; set; }
        public DateTime EndOfSubscription { get; set; }
        public int Points { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int CustomerId { get; set; }
    }

}
