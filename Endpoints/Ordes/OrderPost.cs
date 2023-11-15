using System.Security.Claims;
using _4_IWantApp.Domain.Orders;
using _4_IWantApp.Domain.Products;
using _4_IWantApp.Endpoints.DTO;
using _4_IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace _4_IWantApp.Endpoints.Clients
{
    public class OrderPost
    {
        public static string Template => "/orders";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [Authorize]
        public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http, ApplicationDbContext context)
        {
            string clientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string clientName = http.User.Claims.First(c => c.Type == "Name").Value;

            var products = new List<Product>();
            var productsFound = context.Products.Where(p => orderRequest.ProductIds
            .Contains(p.Id)).ToList();

            var order = new Order(
                clientId,
                clientName,
                productsFound,
                 orderRequest.DeleveryAdress);


            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return Results.Created($"/orders/{order.Id}", order.Id);

        }
    }
}