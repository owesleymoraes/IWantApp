using _4_IWantApp.Endpoints.Categories;
using _4_IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace _4_IWantApp.Endpoints.Products
{
    public class ProductGetAllShowcase
    {
        public static string Template => "/products/showcase";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
        [AllowAnonymous]
        public static async Task<IResult> Action(int? page, int? row, string? orderBy, ApplicationDbContext context)
        {
            if (!page.HasValue) page = 1;
            if (!row.HasValue) row = 10;
            if (string.IsNullOrEmpty(orderBy)) orderBy = "name";

            IQueryable<Domain.Products.Product> queryBase = context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active);


            if (orderBy == "name")
            {
                queryBase = queryBase.OrderBy(p => p.Name);
            }
            else
            {
                queryBase = queryBase.OrderBy(p => p.Price);
            }

            IQueryable<Domain.Products.Product> queryFilter = queryBase.Skip((page.Value - 1) * row.Value).Take(row.Value);


            var products = queryFilter.ToList();

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