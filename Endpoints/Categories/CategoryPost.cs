using _4_IWantApp.Domain.Products;
using _4_IWantApp.Infra.Data;

namespace _4_IWantApp.Endpoints.Categories
{
    public partial class CategoryPost
    {
        public static string Template => "/categories";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
        {
            var category = new Category
            {
                Name = categoryRequest.Name
            };

            context.Categories.Add(category);
            context.SaveChanges();

            return Results.Created($"/categories/{category.Id}", category.Id);

        }
    }
}