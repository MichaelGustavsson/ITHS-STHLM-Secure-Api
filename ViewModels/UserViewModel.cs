namespace secure_api.ViewModels
{
  public class UserViewModel
  {
    public string? UserName { get; set; }
    public DateTime Expires { get; set; }
    public string? Token { get; set; }
  }
}