using System.ComponentModel.DataAnnotations;

namespace WebbApp.Models;

public class SubscribeModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-mail addres", Prompt ="Your Email")]
    public string Email { get; set; } = null!;
    public bool DailyNewsLetter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool WeekinReview { get; set; }
    public bool EventUpdates { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool Podcasts { get; set; }

}
