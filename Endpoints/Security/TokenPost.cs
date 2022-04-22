using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace IWantApp.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action; 

    [AllowAnonymous]
    public static async Task<IResult> Action(
        LoginRequest loginRequest, 
        IConfiguration configuration, 
        UserManager<IdentityUser> userManager,
        ILogger<TokenPost> log,
        IWebHostEnvironment environment)
    {
        log.LogInformation("Getting Token");
        // log.LogWarning("Warning");
        // log.LogError("Error");

        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
        if(user == null)
            Results.BadRequest();
        if (!await userManager.CheckPasswordAsync(user, loginRequest.Password))
            Results.BadRequest();

        var claims = await userManager.GetClaimsAsync(user);
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginRequest.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        });
        subject.AddClaims(claims);

        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
        DateTime expire = DateTime.UtcNow.AddMinutes(2);

        if (environment.IsDevelopment())
            expire = DateTime.UtcNow.AddYears(1);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JwtBearerTokenSettings:Audience"],
            Issuer = configuration["JwtBearerTokenSettings:Issuer"],

            Expires = expire
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}