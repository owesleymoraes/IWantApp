using System.Security.Claims;
using _4_IWantApp.Endpoints.DTO;
using Microsoft.AspNetCore.Identity;

namespace _4_IWantApp.Endpoints.Employees
{
    public class EmployeePost
    {
        public static string Template => "/employee";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
        {
            IdentityUser user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };

            IdentityResult result = userManager.CreateAsync(user, employeeRequest.Password).Result;

            if (!result.Succeeded)
            {
                return Results.BadRequest(result.Errors.First());
            }

            IdentityResult claimResult = userManager
            .AddClaimAsync(user, new Claim("EmployeeCode", employeeRequest.EmployeeCode))
            .Result;

            if (!claimResult.Succeeded)
            {
                return Results.BadRequest(claimResult.Errors.First());
            }

            claimResult = userManager
          .AddClaimAsync(user, new Claim("Name", employeeRequest.Name))
          .Result;

            if (!claimResult.Succeeded)
            {
                return Results.BadRequest(claimResult.Errors.First());
            }

            return Results.Created($"/employees/{user.Id}", user.Id);

        }
    }
}