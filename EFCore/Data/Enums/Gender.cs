using System.ComponentModel.DataAnnotations;

namespace EFCore.Data.Enums
{
    public enum Gender
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female,
        [Display(Name = "Unknown")]
        UnKnown
    }
}