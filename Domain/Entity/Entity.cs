namespace IWantApp.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity(string name, string createdBy, DateTime createOn, string editedBy, DateTime editedOn)
        {
            Id = new Guid();
            Name = name;
            CreatedBy = createdBy;
            CreateOn = createOn;
            EditedBy = editedBy;
            EditedOn = editedOn;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreateOn { get; private set; }
        public string EditedBy { get; private set; }
        public DateTime EditedOn { get; private set; }
    }
}