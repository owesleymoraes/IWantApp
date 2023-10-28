using _4_IWantApp.Domain.Products;
using _4_IWantApp.Infra.Data;

namespace _4_IWantApp.Endpoints.Categories
{
    public class CategoryGetAll
    {
        public static string Template => "/categories";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(ApplicationDbContext context)
        {
            List<Category> categories = context.Categories.ToList();

            IEnumerable<CategoryResponse> response = categories
             .Select(item => new CategoryResponse { Id = item.Id, Name = item.Name, Active = item.Active });

            return Results.Ok(response);

        }
    }
}