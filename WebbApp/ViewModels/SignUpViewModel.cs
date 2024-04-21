using WebbApp.Models;

namespace WebbApp.ViewModels;

public class SignUpViewModel
{
    public string Title { get; set; } = "Sign Up";
    public SignUpModel Form { get; set; } = new SignUpModel();
    public string? ErrorMassage {  get; set; }   
}
