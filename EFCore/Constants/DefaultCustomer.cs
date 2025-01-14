using Web.Api.Data.Entities.Enums;

namespace Web.Api.Constants
{
    public record DefaultCustomer
    {
        public const string Name = $"{nameof(DefaultCustomer)}";
        public const string UserName = $"{Name}3535";
        public const string Email = $"{UserName}@mg-control.com";
        public const string Password = $"{UserName}.Asd@123";
        public const string PhoneNumber = Constants.PhoneNumber;
        public const string Facebook = Constants.Facebook;
        public const CustomerType Type = CustomerType.Person;
        public const CustomerGender Gender = CustomerGender.Male;
        public const string Country = Constants.Country;
        public const string Admin = Constants.Admin;
        public const string Comments = "No comments";
    }
}

