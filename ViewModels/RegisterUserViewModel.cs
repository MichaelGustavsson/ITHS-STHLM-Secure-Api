using System.ComponentModel.DataAnnotations;

namespace secure_api.ViewModels
{
  public class RegisterUserViewModel
  {
    [Required]
    [EmailAddress(ErrorMessage = "Felaktig e-post adress")]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool IsAdmin { get; set; } = false;
  }
}