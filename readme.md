# Secure-Api Starter

Detta är ett start projekt för vår genomgång av Token baserad säkerhet. Det enda som är gjort här är att en AuthController klass är skapad och en HTTPPost metod är tillagd med en enkel inloggningskontroll.
**Som är hårdkodad😁!**

Dessutom finns i mappen ViewModels en klass LoginViewModel för att kunna skicka in UserName och Password till ovanstående POST metod.

I AuthController är även metoden CreateJwtToken skapad med grundläggande logik för att generera ett token som returneras i Login metoden.

#### Glöm inte att köra kommandot dotnet restore

Jag har en .gitignore fil som inte skickar upp bin eller obj mapparna.
