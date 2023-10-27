namespace _4_IWantApp.Domain.Products
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; } = true;
    }
}