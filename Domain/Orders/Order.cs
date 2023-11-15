using _4_IWantApp.Domain.Products;
using Flunt.Validations;

namespace _4_IWantApp.Domain.Orders
{
    public class Order : BaseEntity
    {
        public string ClientId { get; private set; }
        public string ClientName { get; private set; }
        public List<Product> Products { get; private set; }
        public decimal Total { get; private set; }
        public string DeliveryAddress { get; private set; }

        private Order() { }

        public Order(
            string clientId,
            string clientName,
            List<Product> products,
            string deliveryAddress

            )
        {
            ClientId = clientId;
            ClientName = clientName;
            Products = products;
            DeliveryAddress = deliveryAddress;

            CreatedBy = clientName;
            EditedBy = clientName;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Total = 0;
            foreach (var item in Products)
            {
                Total += item.Price;

            }

            Validate();

        }

        private void Validate()
        {
            var contract = new Contract<Order>()

            .IsNotNull(ClientId, "ClientId", "ClientId not found")
            .IsNotNull(Products, "Products", "Products not found");
            AddNotifications(contract);

        }

    }
}