namespace SpaceEventService;

public class Event
{
    private static int _counter = 0;
    public int Id { get; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartAt { get; set; }

    public DateTime EndAt { get; set; }

    public Event(string tittle,  DateTime startAt, DateTime endAt, string? description = "")
    { 
        Id= _counter++;
        Title = tittle;
        Description = description;
        StartAt = startAt;
        EndAt = endAt;
    }
}