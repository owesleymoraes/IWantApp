namespace _4_IWantApp.Domain.Products
{
    public class Product : BaseEntity
    {
        public Category Category { get; set; }
        public string Description { get; set; }
        public bool HasStock { get; set; }

    }
}