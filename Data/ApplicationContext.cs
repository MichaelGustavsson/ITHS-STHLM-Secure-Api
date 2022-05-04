using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace secure_api.Data
{
  public class ApplicationContext : IdentityDbContext
  {
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
  }
}