using IWantApp.Domain.Entities;
namespace IWantApp.Domain.Products
{
    public class Category : Entity
    {
        public Category(
            string name, 
            string createdBy, 
            DateTime createOn, 
            string editedBy, 
            DateTime editedOn) 
        : base(name, createdBy, createOn, editedBy, editedOn){}
        
        public string Name { get; private set; }
    }
}