namespace PersonDirectory.Application.Queries.Dtos
{
    public class PersonReportDto
    {
        public string Name { get; set; }
        public int RelatedPersonsCount { get; set; }
        public string RelationshipType { get; set; }
    }
}
