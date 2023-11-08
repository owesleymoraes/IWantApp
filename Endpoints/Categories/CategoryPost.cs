using _4_IWantApp.Domain.Products;
using _4_IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace _4_IWantApp.Endpoints.Categories
{
    public class CategoryPost
    {
        public static string Template => "/categories";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;
        
        [Authorize]
        public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
        {
            var category = new Category(categoryRequest.Name, "Wesley", "Wesley");

            if (!category.IsValid)
            {
                return Results.BadRequest(category.Notifications);
            }
            context.Categories.Add(category);
            context.SaveChanges();

            return Results.Created($"/categories/{category.Id}", category.Id);

        }
    }
}