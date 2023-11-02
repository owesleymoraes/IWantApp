using _4_IWantApp.Domain.Products;
using _4_IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace _4_IWantApp.Endpoints.Categories
{
    public class CategoryPut
    {
        public static string Template => "/categories/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, ApplicationDbContext context)
        {
            Category category = context.Categories.Where(item => item.Id == id).FirstOrDefault();

            if (category == null)
            {
                return Results.BadRequest();
            }

            category.EditInfo(categoryRequest.Name, categoryRequest.Active);

            if (!category.IsValid)
            {
                return Results.ValidationProblem(category.Notifications.ConvertToProblemDetail());
            }

            context.SaveChanges();

            return Results.Ok();

        }
    }
}