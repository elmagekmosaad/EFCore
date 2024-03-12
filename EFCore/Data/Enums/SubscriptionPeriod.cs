using System.ComponentModel.DataAnnotations;

namespace EFCore.Data.Enums
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