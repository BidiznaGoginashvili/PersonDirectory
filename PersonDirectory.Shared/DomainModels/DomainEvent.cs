namespace PersonDirectory.Shared.DomainModels
{
    public class DomainEvent
    {
        public string? EventType { get; set; }

        public int AggregateRootId { get; set; }

        public DateTimeOffset OccuredOn { get; set; }

        public void ChangeDetails(string eventType,
                                  int aggregateRootId,
                                  DateTimeOffset occuredOn)
        {
            OccuredOn = occuredOn;
            EventType = eventType;
            AggregateRootId = aggregateRootId;
        }
    }
}
