using System.ComponentModel.DataAnnotations;

namespace Web.Api.Data.Entities.Enums
{
    public enum SubscriptionPeriod
    {
        [Display(Name = "Blocked")]
        Blocked,
        [Display(Name = "1 Month")]
        Month,
        [Display(Name = "6 Months")]
        SixMonths,
        [Display(Name = "1 Year")]
        Year,
        [Display(Name = "5 Years")]
        FiveYears
    }
}