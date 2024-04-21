using System.ComponentModel.DataAnnotations;

namespace WebbApp.Models;

public class AccountDetailsBasicInfoModel
{

    [Display(Name = "First Name", Prompt = "Enter your first name")]
    [Required]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Enter your Last name")]
    [Required]
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email")]
    [Required]
    public string Email { get; set; } = null!;

    //[DataType(DataType.PhoneNumber)]
    [Display(Name = "PhoneNumber", Prompt = "Enter your phone")]
    public string? PhoneNumber { get; set; }

    //[DataType(DataType.MultilineText)]
    [Display(Name = "Bio", Prompt = "Add a short bio...")]
    public string? Bio { get; set; }

}

