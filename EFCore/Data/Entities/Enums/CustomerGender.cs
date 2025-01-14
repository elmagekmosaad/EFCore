using System.ComponentModel.DataAnnotations;

namespace Web.Api.Data.Entities.Enums
{
    public enum CustomerGender
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female,
        [Display(Name = "Unknown")]
        UnKnown
    }
}