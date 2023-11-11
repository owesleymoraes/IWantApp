using Flunt.Validations;

namespace _4_IWantApp.Domain.Products
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public bool Active { get; private set; } = true;

        private void Validate()
        {
            var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy", "Nome do criador da categoria é obrigatório")
            .IsNotNullOrEmpty(EditedBy, "EditedBy", "Editar é um campo obrigatório");
            AddNotifications(contract);
        }
        public Category(string name, string createdBy, string editedBy)
        {
            Name = name;
            Active = true;
            EditedBy = editedBy;
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Validate();

        }
        public void EditInfo(string name, bool active, string editedBy)
        {
            Active = active;
            Name = name;
            EditedBy = editedBy;
            EditedOn = DateTime.Now;

            Validate();
        }

    }
}