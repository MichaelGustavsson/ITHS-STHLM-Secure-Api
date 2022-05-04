using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using secure_api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Steg 1. Konfigurera vårt datacontext till att använda Sqlite...
builder.Services.AddDbContext<ApplicationContext>(options =>
{
  options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
});

// Steg 2.
// Sätta upp Identity hantering och ange vilket datacontext som skall användas
// För att lagra användare, roller och claims
// Vi kan dessutom sätta upp regler för t ex lösenord och utlåsningsprinciper...
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
      options.Password.RequireLowercase = true;
      options.Password.RequireUppercase = true;
      options.Password.RequiredLength = 6;
      options.Password.RequireNonAlphanumeric = false;

      options.User.RequireUniqueEmail = true;

      options.Lockout.MaxFailedAccessAttempts = 5;
      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    }
)
.AddEntityFrameworkStores<ApplicationContext>();

// Konfigura Authentication
builder.Services.AddAuthentication(options =>
{
  // DefaultAuthenticationScheme and DefaultChallengeScheme
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("apiKey"))
      ),
    ValidateLifetime = true,
    ValidateAudience = false,
    ValidateIssuer = false,
    ClockSkew = TimeSpan.Zero
  };
});

// Konfigurera och skapa "Policies"...
builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("Admins", policy => policy.RequireClaim("Admin"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

/* Pipeline definierar ordning på metodernas mottagning och utskick*/
/* Placeras Middleware */

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
