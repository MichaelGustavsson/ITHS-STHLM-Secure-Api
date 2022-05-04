using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace secure_api.Data
{
  // IdentityDbContext är en härledd klass ifrån DbContext
  // med stöd för inloggning och rollhantering via automatiska tabeller
  // som vi får via IdentityDbContext
  public class ApplicationContext : IdentityDbContext
  {
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
  }
}