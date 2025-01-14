
namespace EFCore.Models.Dtos
{
    public class ComputerDto
    {
        public int Id { get; set; }
        public string Hwid { get; set; }
        public string Serial { get; set; }
        public int SubscriptionId { get; set; }
        public int CustomerId { get; set; }
    }

}
