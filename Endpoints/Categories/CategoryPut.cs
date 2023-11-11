using _4_IWantApp.Domain.Products;
using _4_IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace _4_IWantApp.Endpoints.Categories
{
    public class CategoryPut
    {
        public static string Template => "/categories/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static IResult Action([FromRoute] Guid id, HttpContext http, CategoryRequest categoryRequest, ApplicationDbContext context)
        {
            string userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Category category = context.Categories.Where(item => item.Id == id).FirstOrDefault();

            if (category == null)
            {
                return Results.BadRequest();
            }

            category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

            if (!category.IsValid)
            {
                return Results.ValidationProblem(category.Notifications.ConvertToProblemDetail());
            }

            context.SaveChanges();

            return Results.Ok();

        }
    }
}