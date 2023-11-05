using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _4_IWantApp.Endpoints.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace _4_IWantApp.Endpoints.Security
{
    public class TokenPost
    {
        public static string Template => "/token";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager)
        {
            var user = userManager.FindByEmailAsync(loginRequest.Email).Result;

            if (user == null)
            {
                Results.BadRequest();
            }

            if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            {
                Results.BadRequest();
            }

            var key = Encoding.ASCII.GetBytes("A@fderwfQQXCCer34");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, loginRequest.Email),
                }),

                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "IWantApp",
                Issuer = "Issue"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Results.Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }
    }
}