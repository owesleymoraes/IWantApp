using System.Security.Claims;
using _4_IWantApp.Endpoints.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _4_IWantApp.Endpoints.Clients
{
    public class ClientPost
    {
        public static string Template => "/clients";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(ClientRequest clientRequest, HttpContext http, UserManager<IdentityUser> userManager)
        {
            IdentityUser newUser = new IdentityUser { UserName = clientRequest.Email, Email = clientRequest.Email };

            IdentityResult result = await userManager.CreateAsync(newUser, clientRequest.Password);

            if (!result.Succeeded)
            {
                return Results.ValidationProblem(result.Errors.ConvertToProblemDetail());
            }

            var userClaims = new List<Claim>
            {
                new Claim("Cpf", clientRequest.Cpf),
                new Claim("Name", clientRequest.Name),

            };

            IdentityResult claimResult = await userManager
            .AddClaimsAsync(newUser, userClaims);

            if (!claimResult.Succeeded)
            {
                return Results.BadRequest(claimResult.Errors.First());
            }

            if (!claimResult.Succeeded)
            {
                return Results.BadRequest(claimResult.Errors.First());
            }

            return Results.Created($"/clients/{newUser.Id}", newUser.Id);

        }
    }
}