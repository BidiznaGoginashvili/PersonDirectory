namespace PersonDirectory.Shared.DomainModels
{
    public class Entity
    {
        public int Id { get; protected set; }
        public bool Deleted { get;  set; }
        public DateTimeOffset? CreateDate { get; protected set; }
        public DateTimeOffset? LastChangeDate { get; protected set; }
    }
}
