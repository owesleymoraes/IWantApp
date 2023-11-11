using System.Security.Claims;
using _4_IWantApp.Endpoints.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _4_IWantApp.Endpoints.Employees
{
    public class EmployeePost
    {
        public static string Template => "/employee";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
        {
            string userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            IdentityUser newUser = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };

            IdentityResult result = await userManager.CreateAsync(newUser, employeeRequest.Password);

            if (!result.Succeeded)
            {
                return Results.ValidationProblem(result.Errors.ConvertToProblemDetail());
            }

            var userClaims = new List<Claim>
            {
                new Claim("EmployeeCode", employeeRequest.EmployeeCode),
                new Claim("Name", employeeRequest.Name),
                new Claim("CreatedBy", userId)
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

            return Results.Created($"/employees/{newUser.Id}", newUser.Id);

        }
    }
}