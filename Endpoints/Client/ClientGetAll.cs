using System.Security.Claims;
using _4_IWantApp.Endpoints.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _4_IWantApp.Endpoints.Clients
{
    public class ClientGetAll
    {
        public static string Template => "/clients";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(HttpContext http)
        {
            ClaimsPrincipal user = http.User;

            var result = new
            {
                Id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                Name = user.Claims.First(c => c.Type == "Name").Value,
                Cpf = user.Claims.First(c => c.Type == "Cpf").Value,
            };

            return Results.Ok(result);

        }
    }
}