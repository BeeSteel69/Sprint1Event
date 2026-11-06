using System.Threading;

namespace Sprint1;

/// <summary>
/// Класс Event(событие)
/// </summary>
public class Event
{
    /// <summary>
    /// Счётчик для генерации ID события
    /// </summary>
    private static int _counter = 0;

    /// <summary>
    /// ID события
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Название события
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описние события (не обязательно)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата начала события
    /// </summary>
    public DateTime StartAt { get; set; }

    /// <summary>
    /// Дата окончания события
    /// </summary>
    public DateTime EndAt { get; set; }

    /// <summary>
    /// Конструктор класса Event
    /// </summary>
    /// <param name="tittle"></param>
    /// <param name="startAt"></param>
    /// <param name="endAt"></param>
    /// <param name="description"></param>
    public Event(string tittle,  DateTime startAt, DateTime endAt, string? description = "")
    { 
        Id= Interlocked.Increment(ref _counter); ;
        Title = tittle;
        Description = description;
        StartAt = startAt;
        EndAt = endAt;
    }
}