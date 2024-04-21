using System.ComponentModel.DataAnnotations;

namespace WebbApp.Models;

public class AccountDetailsAddressInfoModel
{
    [Display(Name = "Address line 1", Prompt = "Enter your address line")]
    [Required]
    public string AddressLineOne { get; set; } = null!;

    [Display(Name = "Address line 2", Prompt = "Enter your second address line")]
    public string? AddressLineTwo { get; set;  }

    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal code", Prompt = "Enter your postal code")]
    [Required]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City", Prompt = "Enter your city")]
    [Required]
    public string City { get; set; } = null!;

   
}

