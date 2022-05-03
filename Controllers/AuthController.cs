using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using secure_api.ViewModels;

namespace secure_api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {

    [HttpPost]
    public ActionResult Login(LoginViewModel model)
    {
      // Detta är bara en "dummy" kontroll av användare och lösenord
      // Vi kommer att hantera detta via Identity lite senare...
      if (model.UserName == "kalle" && model.Password == "Pa$$w0rd")
      {
        // Vi skapar ett anonymt objekt här för tillfället...
        return Ok(new
        {
          access_token = CreateJwtToken(model.UserName)
        });
      }
      return Unauthorized();
    }
    private string CreateJwtToken(string userName)
    {

      // Kommer att hämtas ifrån AppSettings...
      var key = Encoding.ASCII.GetBytes("Kalle Anka och hans vänner");

      // Skapa en lista av Claims som kommer innehålla
      // information som är av värde för behörighetskontroll...
      var claims = new List<Claim>
      {
          new Claim(ClaimTypes.Name, userName)
      };

      // Skapa ett nytt token...
      var jwt = new JwtSecurityToken(
          claims: claims,
          // notBefore: Från när skall biljetten/token vara giltig.
          // Vi kan sätta detta till en datum i framtiden om biljetten/token
          // skall skapas men inte vara giltig på en gång...
          notBefore: DateTime.Now,
          // Sätt giltighetstiden på biljetten i detta fallet en vecka.
          expires: DateTime.Now.AddDays(7),
          // Skapa en instans av SigningCredential klassen
          // som används för att skapa en hash och signering av biljetten.
          signingCredentials: new SigningCredentials(
              // Vi använder en SymmetricSecurityKey som tar vår hemlighet
              // som argument och sedan talar vi om vilken algoritm som skall
              // användas för att skapa hash värdet.
              new SymmetricSecurityKey(key),
              SecurityAlgorithms.HmacSha512Signature
          )
      );

      // Vi använder klassen JwtSecurityTokenHandler och dess metod WriteToken för att
      // skapa en sträng av vårt token...
      return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
  }
}