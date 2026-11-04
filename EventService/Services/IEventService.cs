namespace Sprint1;

/// <summary>
/// Интерфейс Event
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Получить все события
    /// </summary>
    /// <returns></returns>
    List<Event> GetAllEvents();

    /// <summary>
    /// Получить событие по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Event? GetEventById(int id);

    /// <summary>
    /// Создать событие
    /// </summary>
    /// <param name="newEvent"></param>
    void CreateEvent(Event newEvent);

    /// <summary>
    /// Обновить событие
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateEvent"></param>
    void UpdateEvent(int id, Event updateEvent);
    
    /// <summary>
    /// Удалить событие
    /// </summary>
    /// <param name="id"></param>
    void DeleteEvent(int id);
}
