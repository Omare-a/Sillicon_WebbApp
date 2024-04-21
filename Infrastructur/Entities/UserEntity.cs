using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructur.Entities;

public class UserEntity : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ProfileImage { get; set; } = "avatar.jpg";
    public string? Bio { get; set; }

    public int? AddressId { get; set; }
    public AdressEntity? Address { get; set; }
}

