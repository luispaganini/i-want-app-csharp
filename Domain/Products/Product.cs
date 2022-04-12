using IWantApp.Domain.Entities;

namespace IWantApp.Domain.Products
{
    public class Product : Entity
    {
        public Product(
            string name, 
            string createdBy, 
            DateTime createOn, 
            string editedBy, 
            DateTime editedOn) 
        : base(name, createdBy, createOn, editedBy, editedOn){}
        public string Name { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public string Description { get; private set; }
        public bool HasStock { get; private set; }
    }
}