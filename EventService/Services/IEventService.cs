using FluentResults;

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
    /// Создать событие через Dto
    /// </summary>
    /// <param name="eventDto"></param>
    Event CreateEvent(EventDto eventDto);

    /// <summary>
    /// Обновить событие через Dto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventDto"></param>
    Result<Event> UpdateEvent(int id, EventDto eventDto);

    /// <summary>
    /// Удалить событие
    /// </summary>
    /// <param name="id"></param>
    bool DeleteEvent(int id);
}
