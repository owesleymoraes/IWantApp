
using System.Security.Claims;
using _4_IWantApp.Domain.Products;
using _4_IWantApp.Endpoints.Categories;
using _4_IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace _4_IWantApp.Endpoints.Products
{
    public class ProductPost
    {
        public static string Template => "/products";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action(ProductRequest productRequest, HttpContext http, ApplicationDbContext context)
        {
            string userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == productRequest.CategoryId);
            var product = new Product(productRequest.Name, category, productRequest.Description, productRequest.HasStock, userId, productRequest.price);

            if (!product.IsValid)
            {
                return Results.BadRequest(product.Notifications.ConvertToProblemDetail());
            }
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return Results.Created($"/products/{category.Id}", category.Id);

        }
    }

}
