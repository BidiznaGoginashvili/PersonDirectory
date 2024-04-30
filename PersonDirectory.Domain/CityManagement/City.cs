using PersonDirectory.Shared.DomainModels;

namespace PersonDirectory.Domain.CityManagement
{
    public class City : AggregateRoot
    {
        public City(string name) =>
            Name = name;

        public string Name { get; set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
