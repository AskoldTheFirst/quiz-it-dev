//using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using System.IdentityModel.Tokens.Jwt;

//namespace KramarDev.Quiz.BLL;

//public class TokenService
//{
//    private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
//    private readonly UserManager<IdentityUser> _userManager;

//    public TokenService(UserManager<IdentityUser> userManager, Microsoft.Extensions.Configuration.IConfiguration config)
//    {
//        _userManager = userManager;
//        _config = config;
//    }

//    public async Task<string> GenerateTokenAsync(IdentityUser user)
//    {
//        var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Name, user.UserName)
//            };

//        var roles = await _userManager.GetRolesAsync(user);
//        foreach (var role in roles)
//        {
//            claims.Add(new Claim(ClaimTypes.Role, role));
//        }

//        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:TokenKey"]));
//        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

//        var tokenOptions = new JwtSecurityToken(
//            issuer: null,
//            audience: null,
//            claims: claims,
//            expires: DateTime.Now.AddDays(7),
//            signingCredentials: creds
//        );

//        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
//    }
//}
