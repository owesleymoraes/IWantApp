using _4_IWantApp.Endpoints.Categories;
using _4_IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace _4_IWantApp.Endpoints.Products
{
    public class ProductGetAll
    {
        public static string Template => "/products";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action(ApplicationDbContext context)
        {
            List<Domain.Products.Product> products = context.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();
            IEnumerable<ProductResponse> results = products.Select(p => new ProductResponse(
                 p.Name,
                 p.Category.Name,
                 p.Description,
                 p.HasStock,
                 p.Active,
                 p.Price
                 ));
            return Results.Ok(results);

        }

    }
}