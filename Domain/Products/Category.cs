using Flunt.Validations;

namespace _4_IWantApp.Domain.Products
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; } = true;

        public Category(string name, string createdBy, string editedBy)
        {
            var contract = new Contract<Category>()
             .IsNotNullOrEmpty(name, "Name", "Nome é obrigatório")
             .IsNotNullOrEmpty(createdBy, "CreatedBy", "Nome do criador da categoria é obrigatório")
             .IsNotNullOrEmpty(editedBy, "EditedBy", "Editar é um campo obrigatório");
            AddNotifications(contract);

            Name = name;
            Active = true;
            EditedBy = editedBy;
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

        }

    }
}